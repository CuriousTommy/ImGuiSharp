// ReSharper disable once CheckNamespace
namespace ImGui;
public unsafe partial struct ImGuiWindow
{
        public byte* Name;
        public uint ID;
        public ImGuiWindowFlags Flags;
        public Vector2 Pos;
        public Vector2 Size;
        public Vector2 SizeFull;
        public Vector2 ContentSize;
        public Vector2 ContentSizeIdeal;
        public Vector2 ContentSizeExplicit;
        public Vector2 WindowPadding;
        public float WindowRounding;
        public float WindowBorderSize;
        public int NameBufLen;
        public uint MoveId;
        public uint ChildId;
        public Vector2 Scroll;
        public Vector2 ScrollMax;
        public Vector2 ScrollTarget;
        public Vector2 ScrollTargetCenterRatio;
        public Vector2 ScrollTargetEdgeSnapDist;
        public Vector2 ScrollbarSizes;
        public byte ScrollbarX;
        public byte ScrollbarY;
        public byte Active;
        public byte WasActive;
        public byte WriteAccessed;
        public byte Collapsed;
        public byte WantCollapseToggle;
        public byte SkipItems;
        public byte Appearing;
        public byte Hidden;
        public byte IsFallbackWindow;
        public byte IsExplicitChild;
        public byte HasCloseButton;
        public sbyte ResizeBorderHeld;
        public short BeginCount;
        public short BeginOrderWithinParent;
        public short BeginOrderWithinContext;
        public short FocusOrder;
        public uint PopupId;
        public sbyte AutoFitFramesX;
        public sbyte AutoFitFramesY;
        public sbyte AutoFitChildAxises;
        public byte AutoFitOnlyGrows;
        public ImGuiDir AutoPosLastDirection;
        public sbyte HiddenFramesCanSkipItems;
        public sbyte HiddenFramesCannotSkipItems;
        public sbyte HiddenFramesForRenderOnly;
        public sbyte DisableInputsFrames;
        public ImGuiCond SetWindowPosAllowFlags;
        public ImGuiCond SetWindowSizeAllowFlags;
        public ImGuiCond SetWindowCollapsedAllowFlags;
        public Vector2 SetWindowPosVal;
        public Vector2 SetWindowPosPivot;
        public ImVector IDStack;
        public ImGuiWindowTempData DC;
        public ImRect OuterRectClipped;
        public ImRect InnerRect;
        public ImRect InnerClipRect;
        public ImRect WorkRect;
        public ImRect ParentWorkRect;
        public ImRect ClipRect;
        public ImRect ContentRegionRect;
        public ImVec2ih HitTestHoleSize;
        public ImVec2ih HitTestHoleOffset;
        public int LastFrameActive;
        public float LastTimeActive;
        public float ItemWidthDefault;
        public ImGuiStorage StateStorage;
        public ImVector ColumnsStorage;
        public float FontWindowScale;
        public int SettingsOffset;
        public ImDrawList* DrawList;
        public ImDrawList DrawListInst;
        public ImGuiWindow* ParentWindow;
        public ImGuiWindow* ParentWindowInBeginStack;
        public ImGuiWindow* RootWindow;
        public ImGuiWindow* RootWindowPopupTree;
        public ImGuiWindow* RootWindowForTitleBarHighlight;
        public ImGuiWindow* RootWindowForNav;
        public ImGuiWindow* NavLastChildNavWindow;
        public uint NavLastIds0;
        public uint NavLastIds1;
        public uint NavLastIds2;
        public ImRect NavRectRel0;
        public ImRect NavRectRel1;
        public ImRect NavRectRel2;
        public int MemoryDrawListIdxCapacity;
        public int MemoryDrawListVtxCapacity;
        public byte MemoryCompacted;
}
public unsafe partial struct ImGuiWindowPtr
{
    public ImGuiWindow* NativePtr { get; }
    public ImGuiWindowPtr(ImGuiWindow* nativePtr) => NativePtr = nativePtr;
    public ImGuiWindowPtr(IntPtr nativePtr) => NativePtr = (ImGuiWindow*)nativePtr;
    public static implicit operator ImGuiWindowPtr(ImGuiWindow* nativePtr) => new (nativePtr);
    public static implicit operator ImGuiWindow* (ImGuiWindowPtr wrappedPtr) => wrappedPtr.NativePtr;
    public static implicit operator ImGuiWindowPtr(IntPtr nativePtr) => new (nativePtr);
    public RangeAccessor<uint> NavLastIds => new (&NativePtr->NavLastIds0, 2);
    public RangeAccessor<ImRect> NavRectRel => new (&NativePtr->NavRectRel0, 2);
}
