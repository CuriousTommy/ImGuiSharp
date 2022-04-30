// ReSharper disable once CheckNamespace
namespace ImGuiSharp;

public unsafe partial struct ImDrawList
{
    public ImVector CmdBuffer;
    public ImVector IdxBuffer;
    public ImVector VtxBuffer;
    public ImDrawListFlags Flags;
    public uint VtxCurrentIdx;
    public IntPtr Data;
    public char* OwnerName;
    public ImDrawVert* VtxWritePtr;
    public ImDrawIdx* IdxWritePtr;
    public ImVector ClipRectStack;
    public ImVector TextureIdStack;
    public ImVector Path;
    public ImDrawCmdHeader CmdHeader;
    public ImDrawListSplitter Splitter;
    public float FringeScale;

}
