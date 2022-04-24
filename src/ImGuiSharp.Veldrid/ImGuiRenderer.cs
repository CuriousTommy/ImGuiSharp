﻿using System.Numerics;
using System.Reflection;
using Veldrid;


namespace ImGuiSharp.Veldrid
{
    /// <summary>
    /// Can render draw lists produced by ImGui.
    /// Also provides functions for updating ImGui input.
    /// </summary>
    public class ImGuiRenderer : IDisposable
    {
        private GraphicsDevice _gd;
        private readonly Assembly _assembly;
        private ColorSpaceHandling _colorSpaceHandling;

        // Device objects
        private          DeviceBuffer   _vertexBuffer           = null!;
        private          DeviceBuffer   _indexBuffer            = null!;
        private          DeviceBuffer   _projMatrixBuffer       = null!;
        private          Texture        _fontTexture            = null!;
        private          Shader         _vertexShader           = null!;
        private          Shader         _fragmentShader         = null!;
        private          ResourceLayout _layout                 = null!;
        private          ResourceLayout _textureLayout          = null!;
        private          Pipeline       _pipeline               = null!;
        private          ResourceSet    _mainResourceSet        = null!;
        private          ResourceSet    _fontTextureResourceSet = null!;
        private readonly IntPtr         _fontAtlasId            = (IntPtr)1;
        private          bool           _controlDown;
        private          bool           _shiftDown;
        private          bool           _altDown;

        private          int     _windowWidth;
        private          int     _windowHeight;
        private readonly Vector2 _scaleFactor = Vector2.One;

        // Image trackers
        private readonly Dictionary<TextureView, ResourceSetInfo> _setsByView         = new();
        private readonly Dictionary<Texture, TextureView>         _autoViewsByTexture = new();
        private readonly Dictionary<IntPtr, ResourceSetInfo>      _viewsById          = new();
        private readonly List<IDisposable>                        _ownedResources     = new();
        private          int                                      _lastAssignedId     = 100;
        private          bool                                     _frameBegun;

        /// <summary>
        /// Constructs a new ImGuiRenderer.
        /// </summary>
        /// <param name="gd">The GraphicsDevice used to create and update resources.</param>
        /// <param name="outputDescription">The output format.</param>
        /// <param name="width">The initial width of the rendering target. Can be resized.</param>
        /// <param name="height">The initial height of the rendering target. Can be resized.</param>
        public ImGuiRenderer(GraphicsDevice gd, OutputDescription outputDescription, int width, int height)
            : this(gd, outputDescription, width, height, ColorSpaceHandling.Legacy) { }

        /// <summary>
        /// Constructs a new ImGuiRenderer.
        /// </summary>
        /// <param name="gd">The GraphicsDevice used to create and update resources.</param>
        /// <param name="outputDescription">The output format.</param>
        /// <param name="width">The initial width of the rendering target. Can be resized.</param>
        /// <param name="height">The initial height of the rendering target. Can be resized.</param>
        /// <param name="colorSpaceHandling">Identifies how the renderer should treat vertex colors.</param>
        public ImGuiRenderer(GraphicsDevice gd, OutputDescription outputDescription, int width, int height, ColorSpaceHandling colorSpaceHandling)
        {
            _gd = gd;
            _assembly = typeof(ImGuiRenderer).GetTypeInfo().Assembly;
            _colorSpaceHandling = colorSpaceHandling;
            _windowWidth = width;
            _windowHeight = height;

            var context = ImGui.CreateContext();
            ImGui.SetCurrentContext(context);

            ImGui.GetIO().Fonts.AddFontDefault();

            CreateDeviceResources(gd, outputDescription);
            SetOpenTKKeyMappings();

            SetPerFrameImGuiData(1f / 60f);

            ImGui.NewFrame();
            _frameBegun = true;
        }

        public void WindowResized(int width, int height)
        {
            _windowWidth = width;
            _windowHeight = height;
        }

        public void DestroyDeviceObjects()
        {
            Dispose();
        }

        public void CreateDeviceResources(GraphicsDevice gd, OutputDescription outputDescription)
            => CreateDeviceResources(gd, outputDescription, _colorSpaceHandling);
        public void CreateDeviceResources(GraphicsDevice gd, OutputDescription outputDescription, ColorSpaceHandling colorSpaceHandling)
        {
            _gd = gd;
            _colorSpaceHandling = colorSpaceHandling;
            var factory = gd.ResourceFactory;
            _vertexBuffer = factory.CreateBuffer(new BufferDescription(10000, BufferUsage.VertexBuffer | BufferUsage.Dynamic));
            _vertexBuffer.Name = "ImGui.NET Vertex Buffer";
            _indexBuffer = factory.CreateBuffer(new BufferDescription(2000, BufferUsage.IndexBuffer | BufferUsage.Dynamic));
            _indexBuffer.Name = "ImGui.NET Index Buffer";

            _projMatrixBuffer = factory.CreateBuffer(new BufferDescription(64, BufferUsage.UniformBuffer | BufferUsage.Dynamic));
            _projMatrixBuffer.Name = "ImGui.NET Projection Buffer";

            var vertexShaderBytes = LoadEmbeddedShaderCode(gd.ResourceFactory, "imgui-vertex", ShaderStages.Vertex, _colorSpaceHandling);
            var fragmentShaderBytes = LoadEmbeddedShaderCode(gd.ResourceFactory, "imgui-frag", ShaderStages.Fragment, _colorSpaceHandling);
            _vertexShader = factory.CreateShader(new ShaderDescription(ShaderStages.Vertex, vertexShaderBytes, _gd.BackendType == GraphicsBackend.Vulkan ? "main" : "VS"));
            _fragmentShader = factory.CreateShader(new ShaderDescription(ShaderStages.Fragment, fragmentShaderBytes, _gd.BackendType == GraphicsBackend.Vulkan ? "main" : "FS"));

            var vertexLayouts = new VertexLayoutDescription[]
            {
                new(
                    new VertexElementDescription("in_position", VertexElementSemantic.Position, VertexElementFormat.Float2),
                    new VertexElementDescription("in_texCoord", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2),
                    new VertexElementDescription("in_color", VertexElementSemantic.Color, VertexElementFormat.Byte4_Norm))
            };

            _layout = factory.CreateResourceLayout(new ResourceLayoutDescription(
                new ResourceLayoutElementDescription("ProjectionMatrixBuffer", ResourceKind.UniformBuffer, ShaderStages.Vertex),
                new ResourceLayoutElementDescription("MainSampler", ResourceKind.Sampler, ShaderStages.Fragment)));
            _textureLayout = factory.CreateResourceLayout(new ResourceLayoutDescription(
                new ResourceLayoutElementDescription("MainTexture", ResourceKind.TextureReadOnly, ShaderStages.Fragment)));

            var pd = new GraphicsPipelineDescription(
                                                     BlendStateDescription.SingleAlphaBlend,
                                                     new DepthStencilStateDescription(false, false, ComparisonKind.Always),
                                                     new RasterizerStateDescription(FaceCullMode.None, PolygonFillMode.Solid, FrontFace.Clockwise, true, true),
                                                     PrimitiveTopology.TriangleList,
                                                     new ShaderSetDescription(
                                                                              vertexLayouts,
                                                                              new[] { _vertexShader, _fragmentShader },
                                                                              new[]
                                                                              {
                                                                                  new SpecializationConstant(0, gd.IsClipSpaceYInverted),
                                                                                  new SpecializationConstant(1, _colorSpaceHandling == ColorSpaceHandling.Legacy),
                                                                              }),
                                                     new ResourceLayout[] { _layout, _textureLayout },
                                                     outputDescription,
                                                     ResourceBindingModel.Default);
            _pipeline = factory.CreateGraphicsPipeline(ref pd);

            _mainResourceSet = factory.CreateResourceSet(new ResourceSetDescription(_layout,
                _projMatrixBuffer,
                gd.PointSampler));

            RecreateFontDeviceTexture(gd);
        }

        /// <summary>
        /// Gets or creates a handle for a texture to be drawn with ImGui.
        /// Pass the returned handle to Image() or ImageButton().
        /// </summary>
        public IntPtr GetOrCreateImGuiBinding(ResourceFactory factory, TextureView textureView)
        {
            if (_setsByView.TryGetValue(textureView, out var rsi))
            {
                return rsi.ImGuiBinding;
            }

            var resourceSet = factory.CreateResourceSet(new ResourceSetDescription(_textureLayout, textureView));
            rsi = new ResourceSetInfo(GetNextImGuiBindingID(), resourceSet);

            _setsByView.Add(textureView, rsi);
            _viewsById.Add(rsi.ImGuiBinding, rsi);
            _ownedResources.Add(resourceSet);

            return rsi.ImGuiBinding;
        }

        public void RemoveImGuiBinding(TextureView textureView)
        {
            if (_setsByView.TryGetValue(textureView, out var rsi))
            {
                _setsByView.Remove(textureView);
                _viewsById.Remove(rsi.ImGuiBinding);
                _ownedResources.Remove(rsi.ResourceSet);
                rsi.ResourceSet.Dispose();
            }
        }

        private IntPtr GetNextImGuiBindingID()
        {
            var newID = _lastAssignedId++;
            return (IntPtr)newID;
        }

        /// <summary>
        /// Gets or creates a handle for a texture to be drawn with ImGui.
        /// Pass the returned handle to Image() or ImageButton().
        /// </summary>
        public IntPtr GetOrCreateImGuiBinding(ResourceFactory factory, Texture texture)
        {
            if (!_autoViewsByTexture.TryGetValue(texture, out var textureView))
            {
                textureView = factory.CreateTextureView(texture);
                _autoViewsByTexture.Add(texture, textureView);
                _ownedResources.Add(textureView);
            }

            return GetOrCreateImGuiBinding(factory, textureView);
        }

        public void RemoveImGuiBinding(Texture texture)
        {
            if (_autoViewsByTexture.TryGetValue(texture, out var textureView))
            {
                _autoViewsByTexture.Remove(texture);
                _ownedResources.Remove(textureView);
                textureView.Dispose();
                RemoveImGuiBinding(textureView);
            }
        }

        /// <summary>
        /// Retrieves the shader texture binding for the given helper handle.
        /// </summary>
        public ResourceSet GetImageResourceSet(IntPtr imGuiBinding)
        {
            if (!_viewsById.TryGetValue(imGuiBinding, out var rsi))
            {
                throw new InvalidOperationException("No registered ImGui binding with id " + imGuiBinding.ToString());
            }

            return rsi.ResourceSet;
        }

        public void ClearCachedImageResources()
        {
            foreach (var resource in _ownedResources)
            {
                resource.Dispose();
            }

            _ownedResources.Clear();
            _setsByView.Clear();
            _viewsById.Clear();
            _autoViewsByTexture.Clear();
            _lastAssignedId = 100;
        }

        private byte[] LoadEmbeddedShaderCode(
            ResourceFactory factory,
            string name,
            ShaderStages stage,
            ColorSpaceHandling colorSpaceHandling)
        {
            switch (factory.BackendType)
            {
                case GraphicsBackend.Direct3D11:
                {
                    if (stage == ShaderStages.Vertex && colorSpaceHandling == ColorSpaceHandling.Legacy) { name += "-legacy"; }
                    var resourceName = name + ".hlsl.bytes";
                    return GetEmbeddedResourceBytes(resourceName);
                }
                case GraphicsBackend.OpenGL:
                {
                    if (stage == ShaderStages.Vertex && colorSpaceHandling == ColorSpaceHandling.Legacy) { name += "-legacy"; }
                    var resourceName = name + ".glsl";
                    return GetEmbeddedResourceBytes(resourceName);
                }
                case GraphicsBackend.OpenGLES:
                {
                    if (stage == ShaderStages.Vertex && colorSpaceHandling == ColorSpaceHandling.Legacy) { name += "-legacy"; }
                    var resourceName = name + ".glsles";
                    return GetEmbeddedResourceBytes(resourceName);
                }
                case GraphicsBackend.Vulkan:
                {
                    var resourceName = name + ".spv";
                    return GetEmbeddedResourceBytes(resourceName);
                }
                case GraphicsBackend.Metal:
                {
                    var resourceName = name + ".metallib";
                    return GetEmbeddedResourceBytes(resourceName);
                }
                default:
                    throw new NotImplementedException();
            }
        }

        private string GetEmbeddedResourceText(string resourceName)
        {
            using var sr = new StreamReader(_assembly.GetManifestResourceStream(resourceName)!);
            return sr.ReadToEnd();
        }

        private byte[] GetEmbeddedResourceBytes(string resourceName)
        {
            using var s    = _assembly.GetManifestResourceStream(resourceName);
            var       ret  = new byte[s!.Length];
            var       read = s.Read(ret, 0, (int)s.Length);
            return ret;
        }

        /// <summary>
        /// Recreates the device texture used to render text.
        /// </summary>
        public unsafe void RecreateFontDeviceTexture() => RecreateFontDeviceTexture(_gd);

        /// <summary>
        /// Recreates the device texture used to render text.
        /// </summary>
        public unsafe void RecreateFontDeviceTexture(GraphicsDevice gd)
        {
            var io = ImGui.GetIO();
            // Build
            io.Fonts.GetTexDataAsRGBA32(out byte* pixels, out var width, out var height, out var bytesPerPixel);

            // Store our identifier
            io.Fonts.SetTexID(_fontAtlasId);

            _fontTexture?.Dispose();
            _fontTexture = gd.ResourceFactory.CreateTexture(TextureDescription.Texture2D(
                (uint)width,
                (uint)height,
                1,
                1,
                PixelFormat.R8_G8_B8_A8_UNorm,
                TextureUsage.Sampled));
            _fontTexture.Name = "ImGui.NET Font Texture";
            gd.UpdateTexture(
                _fontTexture,
                (IntPtr)pixels,
                (uint)(bytesPerPixel * width * height),
                0,
                0,
                0,
                (uint)width,
                (uint)height,
                1,
                0,
                0);

            _fontTextureResourceSet?.Dispose();
            _fontTextureResourceSet = gd.ResourceFactory.CreateResourceSet(new ResourceSetDescription(_textureLayout, _fontTexture));

            io.Fonts.ClearTexData();
        }

        /// <summary>
        /// Renders the ImGui draw list data.
        /// </summary>
        public unsafe void Render(GraphicsDevice gd, CommandList cl)
        {
            if (_frameBegun)
            {
                _frameBegun = false;
                ImGui.Render();
                RenderImDrawData(ImGui.GetDrawData(), gd, cl);
            }
        }

        /// <summary>
        /// Updates ImGui input and IO configuration state.
        /// </summary>
        public void Update(float deltaSeconds, InputSnapshot snapshot)
        {
            BeginUpdate(deltaSeconds);
            UpdateImGuiInput(snapshot);
            EndUpdate();
        }

        /// <summary>
        /// Called before we handle the input in <see cref="Update(float, InputSnapshot)"/>.
        /// This render ImGui and update the state.
        /// </summary>
        protected void BeginUpdate(float deltaSeconds)
        {
            if (_frameBegun)
            {
                ImGui.Render();
            }

            SetPerFrameImGuiData(deltaSeconds);
        }

        /// <summary>
        /// Called at the end of <see cref="Update(float, InputSnapshot)"/>.
        /// This tells ImGui that we are on the next frame.
        /// </summary>
        protected void EndUpdate()
        {
            _frameBegun = true;
            ImGui.NewFrame();
        }

        /// <summary>
        /// Sets per-frame data based on the associated window.
        /// This is called by Update(float).
        /// </summary>
        private unsafe void SetPerFrameImGuiData(float deltaSeconds)
        {
            var io = ImGui.GetIO();
            io.DisplaySize = new Vector2(
                _windowWidth / _scaleFactor.X,
                _windowHeight / _scaleFactor.Y);
            io.DisplayFramebufferScale = _scaleFactor;
            io.DeltaTime = deltaSeconds; // DeltaTime is in seconds.
        }

        private unsafe void UpdateImGuiInput(InputSnapshot snapshot)
        {
            var io = ImGui.GetIO();

            // Determine if any of the mouse buttons were pressed during this snapshot period, even if they are no longer held.
            var leftPressed = false;
            var middlePressed = false;
            var rightPressed = false;
            for (var i = 0; i < snapshot.MouseEvents.Count; i++)
            {
                var me = snapshot.MouseEvents[i];
                if (me.Down)
                {
                    switch (me.MouseButton)
                    {
                        case MouseButton.Left:
                            leftPressed = true;
                            break;
                        case MouseButton.Middle:
                            middlePressed = true;
                            break;
                        case MouseButton.Right:
                            rightPressed = true;
                            break;
                    }
                }
            }

            io.MouseDown[0] = leftPressed || snapshot.IsMouseDown(MouseButton.Left);
            io.MouseDown[1] = rightPressed || snapshot.IsMouseDown(MouseButton.Right);
            io.MouseDown[2] = middlePressed || snapshot.IsMouseDown(MouseButton.Middle);
            io.MousePos = snapshot.MousePosition;
            io.MouseWheel = snapshot.WheelDelta;

            var keyCharPresses = snapshot.KeyCharPresses;
            for (var i = 0; i < keyCharPresses.Count; i++)
            {
                var c = keyCharPresses[i];
                ImGui.GetIO().AddInputCharacter(c);
            }

            var keyEvents = snapshot.KeyEvents;
            for (var i = 0; i < keyEvents.Count; i++)
            {
                var keyEvent = keyEvents[i];
                io.KeysDown[(int)keyEvent.Key] = keyEvent.Down;
                if (keyEvent.Key == Key.ControlLeft)
                {
                    _controlDown = keyEvent.Down;
                }
                if (keyEvent.Key == Key.ShiftLeft)
                {
                    _shiftDown = keyEvent.Down;
                }
                if (keyEvent.Key == Key.AltLeft)
                {
                    _altDown = keyEvent.Down;
                }
            }

            io.KeyCtrl = _controlDown;
            io.KeyAlt = _altDown;
            io.KeyShift = _shiftDown;
        }

        private static unsafe void SetOpenTKKeyMappings()
        {
            var io = ImGui.GetIO();
            io.KeyMap[(int)ImGuiKey.Tab] = (int)Key.Tab;
            io.KeyMap[(int)ImGuiKey.LeftArrow] = (int)Key.Left;
            io.KeyMap[(int)ImGuiKey.RightArrow] = (int)Key.Right;
            io.KeyMap[(int)ImGuiKey.UpArrow] = (int)Key.Up;
            io.KeyMap[(int)ImGuiKey.DownArrow] = (int)Key.Down;
            io.KeyMap[(int)ImGuiKey.PageUp] = (int)Key.PageUp;
            io.KeyMap[(int)ImGuiKey.PageDown] = (int)Key.PageDown;
            io.KeyMap[(int)ImGuiKey.Home] = (int)Key.Home;
            io.KeyMap[(int)ImGuiKey.End] = (int)Key.End;
            io.KeyMap[(int)ImGuiKey.Delete] = (int)Key.Delete;
            io.KeyMap[(int)ImGuiKey.Backspace] = (int)Key.BackSpace;
            io.KeyMap[(int)ImGuiKey.Enter] = (int)Key.Enter;
            io.KeyMap[(int)ImGuiKey.Escape] = (int)Key.Escape;
            io.KeyMap[(int)ImGuiKey.Space] = (int)Key.Space;
            io.KeyMap[(int)ImGuiKey.A] = (int)Key.A;
            io.KeyMap[(int)ImGuiKey.C] = (int)Key.C;
            io.KeyMap[(int)ImGuiKey.V] = (int)Key.V;
            io.KeyMap[(int)ImGuiKey.X] = (int)Key.X;
            io.KeyMap[(int)ImGuiKey.Y] = (int)Key.Y;
            io.KeyMap[(int)ImGuiKey.Z] = (int)Key.Z;
        }

        private unsafe void RenderImDrawData(ImDrawDataPtr draw_data, GraphicsDevice gd, CommandList cl)
        {
            uint vertexOffsetInVertices = 0;
            uint indexOffsetInElements = 0;

            if (draw_data.CmdListsCount == 0)
            {
                return;
            }

            var totalVBSize = (uint)(draw_data.TotalVtxCount * sizeof(ImDrawVert));
            if (totalVBSize > _vertexBuffer.SizeInBytes)
            {
                _vertexBuffer.Dispose();
                _vertexBuffer = gd.ResourceFactory.CreateBuffer(new BufferDescription((uint)(totalVBSize * 1.5f), BufferUsage.VertexBuffer | BufferUsage.Dynamic));
            }

            var totalIBSize = (uint)(draw_data.TotalIdxCount * sizeof(ushort));
            if (totalIBSize > _indexBuffer.SizeInBytes)
            {
                _indexBuffer.Dispose();
                _indexBuffer = gd.ResourceFactory.CreateBuffer(new BufferDescription((uint)(totalIBSize * 1.5f), BufferUsage.IndexBuffer | BufferUsage.Dynamic));
            }

            for (var i = 0; i < draw_data.CmdListsCount; i++)
            {
                var cmd_list = draw_data.CmdListsRange[i];

                cl.UpdateBuffer(
                    _vertexBuffer,
                    vertexOffsetInVertices * (uint)sizeof(ImDrawVert),
                    cmd_list.VtxBuffer.Data,
                    (uint)(cmd_list.VtxBuffer.Size * sizeof(ImDrawVert)));

                cl.UpdateBuffer(
                    _indexBuffer,
                    indexOffsetInElements * sizeof(ushort),
                    cmd_list.IdxBuffer.Data,
                    (uint)(cmd_list.IdxBuffer.Size * sizeof(ushort)));

                vertexOffsetInVertices += (uint)cmd_list.VtxBuffer.Size;
                indexOffsetInElements += (uint)cmd_list.IdxBuffer.Size;
            }

            // Setup orthographic projection matrix into our constant buffer
            {
                var io = ImGui.GetIO();

                var mvp = Matrix4x4.CreateOrthographicOffCenter(
                                                                0f,
                                                                io.DisplaySize.X,
                                                                io.DisplaySize.Y,
                                                                0.0f,
                                                                -1.0f,
                                                                1.0f);

                _gd.UpdateBuffer(_projMatrixBuffer, 0, ref mvp);
            }

            cl.SetVertexBuffer(0, _vertexBuffer);
            cl.SetIndexBuffer(_indexBuffer, IndexFormat.UInt16);
            cl.SetPipeline(_pipeline);
            cl.SetGraphicsResourceSet(0, _mainResourceSet);

            draw_data.ScaleClipRects(ImGui.GetIO().DisplayFramebufferScale);

            // Render command lists
            var vtx_offset = 0;
            var idx_offset = 0;
            for (var n = 0; n < draw_data.CmdListsCount; n++)
            {
                var cmd_list = draw_data.CmdListsRange[n];
                for (var cmd_i = 0; cmd_i < cmd_list.CmdBuffer.Size; cmd_i++)
                {
                    var pcmd = cmd_list.CmdBuffer[cmd_i];
                    if (pcmd.UserCallback != IntPtr.Zero)
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        if (pcmd.TextureId != IntPtr.Zero)
                        {
                            if (pcmd.TextureId == _fontAtlasId)
                            {
                                cl.SetGraphicsResourceSet(1, _fontTextureResourceSet);
                            }
                            else
                            {
                                cl.SetGraphicsResourceSet(1, GetImageResourceSet(pcmd.TextureId));
                            }
                        }

                        cl.SetScissorRect(
                            0,
                            (uint)pcmd.ClipRect.X,
                            (uint)pcmd.ClipRect.Y,
                            (uint)(pcmd.ClipRect.Z - pcmd.ClipRect.X),
                            (uint)(pcmd.ClipRect.W - pcmd.ClipRect.Y));

                        cl.DrawIndexed(pcmd.ElemCount, 1, pcmd.IdxOffset + (uint)idx_offset, (int)(pcmd.VtxOffset + vtx_offset), 0);
                    }
                }

                idx_offset += cmd_list.IdxBuffer.Size;
                vtx_offset += cmd_list.VtxBuffer.Size;
            }
        }

        /// <summary>
        /// Frees all graphics resources used by the renderer.
        /// </summary>
        public void Dispose()
        {
            _vertexBuffer.Dispose();
            _indexBuffer.Dispose();
            _projMatrixBuffer.Dispose();
            _fontTexture.Dispose();
            _vertexShader.Dispose();
            _fragmentShader.Dispose();
            _layout.Dispose();
            _textureLayout.Dispose();
            _pipeline.Dispose();
            _mainResourceSet.Dispose();
            _fontTextureResourceSet.Dispose();

            foreach (var resource in _ownedResources)
            {
                resource.Dispose();
            }
        }

        private struct ResourceSetInfo
        {
            public readonly IntPtr ImGuiBinding;
            public readonly ResourceSet ResourceSet;

            public ResourceSetInfo(IntPtr imGuiBinding, ResourceSet resourceSet)
            {
                ImGuiBinding = imGuiBinding;
                ResourceSet = resourceSet;
            }
        }
    }
}