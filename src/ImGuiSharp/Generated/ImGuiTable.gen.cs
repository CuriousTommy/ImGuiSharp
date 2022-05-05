// ReSharper disable once CheckNamespace
namespace ImGui;
public unsafe partial struct ImGuiTable
{
        public uint ID;
        public ImGuiTableFlags Flags;
        public void* RawData;
        public ImGuiTableTempData* TempData;
        public byte Columns;
        public byte DisplayOrderToIndex;
        public byte RowCellData;
        public ulong EnabledMaskByDisplayOrder;
        public ulong EnabledMaskByIndex;
        public ulong VisibleMaskByIndex;
        public ulong RequestOutputMaskByIndex;
        public ImGuiTableFlags SettingsLoadedFlags;
        public int SettingsOffset;
        public int LastFrameActive;
        public int ColumnsCount;
        public int CurrentRow;
        public int CurrentColumn;
        public short InstanceCurrent;
        public short InstanceInteracted;
        public float RowPosY1;
        public float RowPosY2;
        public float RowMinHeight;
        public float RowTextBaseline;
        public float RowIndentOffsetX;
        public ImGuiTableRowFlags RowFlags;
        public ImGuiTableRowFlags LastRowFlags;
        public int RowBgColorCounter;
        public uint RowBgColor0;
        public uint RowBgColor1;
        public uint RowBgColor2;
        public uint BorderColorStrong;
        public uint BorderColorLight;
        public float BorderX1;
        public float BorderX2;
        public float HostIndentX;
        public float MinColumnWidth;
        public float OuterPaddingX;
        public float CellPaddingX;
        public float CellPaddingY;
        public float CellSpacingX1;
        public float CellSpacingX2;
        public float LastOuterHeight;
        public float LastFirstRowHeight;
        public float InnerWidth;
        public float ColumnsGivenWidth;
        public float ColumnsAutoFitWidth;
        public float ResizedColumnNextWidth;
        public float ResizeLockMinContentsX2;
        public float RefScale;
        public ImRect OuterRect;
        public ImRect InnerRect;
        public ImRect WorkRect;
        public ImRect InnerClipRect;
        public ImRect BgClipRect;
        public ImRect Bg0ClipRectForDrawCmd;
        public ImRect Bg2ClipRectForDrawCmd;
        public ImRect HostClipRect;
        public ImRect HostBackupInnerClipRect;
        public ImGuiWindow* OuterWindow;
        public ImGuiWindow* InnerWindow;
        public ImGuiTextBuffer ColumnsNames;
        public ImDrawListSplitter* DrawSplitter;
        public ImGuiTableColumnSortSpecs SortSpecsSingle;
        public ImVector SortSpecsMulti;
        public ImGuiTableSortSpecs SortSpecs;
        public sbyte SortSpecsCount;
        public sbyte ColumnsEnabledCount;
        public sbyte ColumnsEnabledFixedCount;
        public sbyte DeclColumnsCount;
        public sbyte HoveredColumnBody;
        public sbyte HoveredColumnBorder;
        public sbyte AutoFitSingleColumn;
        public sbyte ResizedColumn;
        public sbyte LastResizedColumn;
        public sbyte HeldHeaderColumn;
        public sbyte ReorderColumn;
        public sbyte ReorderColumnDir;
        public sbyte LeftMostEnabledColumn;
        public sbyte RightMostEnabledColumn;
        public sbyte LeftMostStretchedColumn;
        public sbyte RightMostStretchedColumn;
        public sbyte ContextPopupColumn;
        public sbyte FreezeRowsRequest;
        public sbyte FreezeRowsCount;
        public sbyte FreezeColumnsRequest;
        public sbyte FreezeColumnsCount;
        public sbyte RowCellDataCurrent;
        public byte DummyDrawChannel;
        public byte Bg2DrawChannelCurrent;
        public byte Bg2DrawChannelUnfrozen;
        public byte IsLayoutLocked;
        public byte IsInsideRow;
        public byte IsInitializing;
        public byte IsSortSpecsDirty;
        public byte IsUsingHeaders;
        public byte IsContextPopupOpen;
        public byte IsSettingsRequestLoad;
        public byte IsSettingsDirty;
        public byte IsDefaultDisplayOrder;
        public byte IsResetAllRequest;
        public byte IsResetDisplayOrderRequest;
        public byte IsUnfrozenRows;
        public byte IsDefaultSizingPolicy;
        public byte MemoryCompacted;
        public byte HostSkipItems;
}
