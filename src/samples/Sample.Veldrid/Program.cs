﻿using System.Numerics;
using ImGuiSharp;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace Sample.Veldrid;

internal static class Program
{
    private static Sdl2Window      _window;
    private static GraphicsDevice  _gd;
    private static CommandList     _cl;
    private static ImGuiController _controller;
    // private static MemoryEditor _memoryEditor;

    // UI state
    private static          float   _f;
    private static          int     _counter;
    private static          int     _dragInt;
    private static readonly Vector3 ClearColor           = new(0.45f, 0.55f, 0.6f);
    private static          bool    _showImGuiDemoWindow = true;
    private static          bool    _showAnotherWindow;
    private static          uint    _sTabBarFlags = (uint)ImGuiTabBarFlags.Reorderable;
    private static readonly bool[]  SOpened       = { true, true, true, true }; // Persistent user state

    static void SetThing(out float i, float val) { i = val; }

    static void Main(string[] args)
    {
        // Create window, GraphicsDevice, and all resources necessary for the demo.
        VeldridStartup.CreateWindowAndGraphicsDevice(
                                                     new WindowCreateInfo(50, 50, 1280, 720, WindowState.Normal, "ImGui.NET Sample Program"),
                                                     new GraphicsDeviceOptions(true, null, true, ResourceBindingModel.Improved, true, true),
                                                     out _window,
                                                     out _gd);
        _window.Resized += () =>
        {
            _gd.MainSwapchain.Resize((uint)_window.Width, (uint)_window.Height);
            _controller.WindowResized(_window.Width, _window.Height);
        };
        _cl = _gd.ResourceFactory.CreateCommandList();
        _controller = new ImGuiController(_gd, _gd.MainSwapchain.Framebuffer.OutputDescription, _window.Width, _window.Height);


        // Main application loop
        while (_window.Exists)
        {
            var snapshot = _window.PumpEvents();
            if (!_window.Exists) { break; }
            _controller.Update(1f / 60f, snapshot); // Feed the input events to our ImGui controller, which passes them through to ImGui.

            SubmitUI();

            _cl.Begin();
            _cl.SetFramebuffer(_gd.MainSwapchain.Framebuffer);
            _cl.ClearColorTarget(0, new RgbaFloat(ClearColor.X, ClearColor.Y, ClearColor.Z, 1f));
            _controller.Render(_gd, _cl);
            _cl.End();
            _gd.SubmitCommands(_cl);
            _gd.SwapBuffers(_gd.MainSwapchain);
        }

        // Clean up Veldrid resources
        _gd.WaitForIdle();
        _controller.Dispose();
        _cl.Dispose();
        _gd.Dispose();
    }

    private static unsafe void SubmitUI()
    {
        // Demo code adapted from the official Dear ImGui demo program:
        // https://github.com/ocornut/imgui/blob/master/examples/example_win32_directx11/main.cpp#L172

        // 1. Show a simple window.
        // Tip: if we don't call ImGui.BeginWindow()/ImGui.EndWindow() the widgets automatically appears in a window called "Debug".
        {
            ImGui.Text("Hello, world!");                                        // Display some text (you can use a format string too)
            ImGui.SliderFloat("float", ref _f, 0, 1, _f.ToString("0.000"));  // Edit 1 float using a slider from 0.0f to 1.0f    
            //ImGui.ColorEdit3("clear color", ref _clearColor);                   // Edit 3 floats representing a color

            ImGui.Text($"Mouse position: {ImGui.GetMousePos()}");

            ImGui.Checkbox("ImGui Demo Window", ref _showImGuiDemoWindow);                 // Edit bools storing our windows open/close state
            ImGui.Checkbox("Another Window", ref _showAnotherWindow);
            if (ImGui.Button("Button"))                                         // Buttons return true when clicked (NB: most widgets return true when edited/activated)
            {
                _counter++;
            }

            ImGui.SameLine(0, -1);
            ImGui.Text($"counter = {_counter}");

            ImGui.DragInt("Draggable Int", ref _dragInt);

            var framerate = ImGui.GetIO().Framerate;
            ImGui.Text($"Application average {1000.0f / framerate:0.##} ms/frame ({framerate:0.#} FPS)");
        }

        // 2. Show another simple window. In most cases you will use an explicit Begin/End pair to name your windows.
        if (_showAnotherWindow)
        {
            ImGui.Begin("Another Window", ref _showAnotherWindow);
            ImGui.Text("Hello from another window!");
            if (ImGui.Button("Close Me"))
            {
                _showAnotherWindow = false;
            }

            ImGui.End();
        }

        // 3. Show the ImGui demo window. Most of the sample code is in ImGui.ShowDemoWindow(). Read its code to learn more about Dear ImGui!
        if (_showImGuiDemoWindow)
        {
            // Normally user code doesn't need/want to call this because positions are saved in .ini file anyway.
            // Here we just want to make the demo initial state a bit more friendly!
            ImGui.SetNextWindowPos(new Vector2(650, 20), ImGuiCond.FirstUseEver);
            ImGui.ShowDemoWindow(ref _showImGuiDemoWindow);
        }
            
        if (ImGui.TreeNode("Tabs"))
        {
            if (ImGui.TreeNode("Basic"))
            {
                var tab_bar_flags = ImGuiTabBarFlags.None;
                if (ImGui.BeginTabBar("MyTabBar", tab_bar_flags))
                {
                    if (ImGui.BeginTabItem("Avocado"))
                    {
                        ImGui.Text("This is the Avocado tab!\nblah blah blah blah blah");
                        ImGui.EndTabItem();
                    }
                    if (ImGui.BeginTabItem("Broccoli"))
                    {
                        ImGui.Text("This is the Broccoli tab!\nblah blah blah blah blah");
                        ImGui.EndTabItem();
                    }
                    if (ImGui.BeginTabItem("Cucumber"))
                    {
                        ImGui.Text("This is the Cucumber tab!\nblah blah blah blah blah");
                        ImGui.EndTabItem();
                    }
                    ImGui.EndTabBar();
                }
                ImGui.Separator();
                ImGui.TreePop();
            }

            if (ImGui.TreeNode("Advanced & Close Button"))
            {
                // Expose a couple of the available flags. In most cases you may just call BeginTabBar() with no flags (0).
                ImGui.CheckboxFlags("ImGuiTabBarFlags_Reorderable", ref _sTabBarFlags, (uint)ImGuiTabBarFlags.Reorderable);
                ImGui.CheckboxFlags("ImGuiTabBarFlags_AutoSelectNewTabs", ref _sTabBarFlags, (uint)ImGuiTabBarFlags.AutoSelectNewTabs);
                ImGui.CheckboxFlags("ImGuiTabBarFlags_NoCloseWithMiddleMouseButton", ref _sTabBarFlags, (uint)ImGuiTabBarFlags.NoCloseWithMiddleMouseButton);
                if ((_sTabBarFlags & (uint)ImGuiTabBarFlags.FittingPolicyMask) == 0)
                {
                    _sTabBarFlags |= (uint)ImGuiTabBarFlags.FittingPolicyDefault;
                }

                if (ImGui.CheckboxFlags("ImGuiTabBarFlags_FittingPolicyResizeDown", ref _sTabBarFlags, (uint)ImGuiTabBarFlags.FittingPolicyResizeDown))
                {
                    _sTabBarFlags &= ~((uint)ImGuiTabBarFlags.FittingPolicyMask ^ (uint)ImGuiTabBarFlags.FittingPolicyResizeDown);
                }

                if (ImGui.CheckboxFlags("ImGuiTabBarFlags_FittingPolicyScroll", ref _sTabBarFlags, (uint)ImGuiTabBarFlags.FittingPolicyScroll))
                {
                    _sTabBarFlags &= ~((uint)ImGuiTabBarFlags.FittingPolicyMask ^ (uint)ImGuiTabBarFlags.FittingPolicyScroll);
                }

                // Tab Bar
                string[] names = { "Artichoke", "Beetroot", "Celery", "Daikon" };

                for (var n = 0; n < SOpened.Length; n++)
                {
                    if (n > 0) { ImGui.SameLine(); }
                    ImGui.Checkbox(names[n], ref SOpened[n]);
                }

                // Passing a bool* to BeginTabItem() is similar to passing one to Begin(): the underlying bool will be set to false when the tab is closed.
                if (ImGui.BeginTabBar("MyTabBar", (ImGuiTabBarFlags)_sTabBarFlags))
                {
                    for (var n = 0; n < SOpened.Length; n++)
                        if (SOpened[n] && ImGui.BeginTabItem(names[n], ref SOpened[n]))
                        {
                            ImGui.Text($"This is the {names[n]} tab!");
                            if ((n & 1) != 0)
                            {
                                ImGui.Text("I am an odd tab.");
                            }

                            ImGui.EndTabItem();
                        }
                    ImGui.EndTabBar();
                }
                ImGui.Separator();
                ImGui.TreePop();
            }
            ImGui.TreePop();
        }

        var io = ImGui.GetIO();
        SetThing(out io.DeltaTime, 2f);
    }
}