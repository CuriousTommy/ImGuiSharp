using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using ImGuiSharp.Structs;

// ReSharper disable once CheckNamespace
namespace ImGuiSharp
{
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
        public fixed uint RowBgColor[2];
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
    public unsafe partial struct ImGuiTablePtr
    {
        public ImGuiTable* NativePtr { get; }
        public ImGuiTablePtr(ImGuiTable* nativePtr) => NativePtr = nativePtr;
        public ImGuiTablePtr(IntPtr nativePtr) => NativePtr = (ImGuiTable*)nativePtr;
        public static implicit operator ImGuiTablePtr(ImGuiTable* nativePtr) => new ImGuiTablePtr(nativePtr);
        public static implicit operator ImGuiTable* (ImGuiTablePtr wrappedPtr) => wrappedPtr.NativePtr;
        public static implicit operator ImGuiTablePtr(IntPtr nativePtr) => new ImGuiTablePtr(nativePtr);
        public ref uint ID => ref Unsafe.AsRef<uint>(&NativePtr->ID);
        public ref ImGuiTableFlags Flags => ref Unsafe.AsRef<ImGuiTableFlags>(&NativePtr->Flags);
        public IntPtr RawData { get => (IntPtr)NativePtr->RawData; set => NativePtr->RawData = (void*)value; }
        public ImGuiTableTempDataPtr TempData => new ImGuiTableTempDataPtr(NativePtr->TempData);
        public ref byte Columns => ref Unsafe.AsRef<byte>(&NativePtr->Columns);
        public ref byte DisplayOrderToIndex => ref Unsafe.AsRef<byte>(&NativePtr->DisplayOrderToIndex);
        public ref byte RowCellData => ref Unsafe.AsRef<byte>(&NativePtr->RowCellData);
        public ref ulong EnabledMaskByDisplayOrder => ref Unsafe.AsRef<ulong>(&NativePtr->EnabledMaskByDisplayOrder);
        public ref ulong EnabledMaskByIndex => ref Unsafe.AsRef<ulong>(&NativePtr->EnabledMaskByIndex);
        public ref ulong VisibleMaskByIndex => ref Unsafe.AsRef<ulong>(&NativePtr->VisibleMaskByIndex);
        public ref ulong RequestOutputMaskByIndex => ref Unsafe.AsRef<ulong>(&NativePtr->RequestOutputMaskByIndex);
        public ref ImGuiTableFlags SettingsLoadedFlags => ref Unsafe.AsRef<ImGuiTableFlags>(&NativePtr->SettingsLoadedFlags);
        public ref int SettingsOffset => ref Unsafe.AsRef<int>(&NativePtr->SettingsOffset);
        public ref int LastFrameActive => ref Unsafe.AsRef<int>(&NativePtr->LastFrameActive);
        public ref int ColumnsCount => ref Unsafe.AsRef<int>(&NativePtr->ColumnsCount);
        public ref int CurrentRow => ref Unsafe.AsRef<int>(&NativePtr->CurrentRow);
        public ref int CurrentColumn => ref Unsafe.AsRef<int>(&NativePtr->CurrentColumn);
        public ref short InstanceCurrent => ref Unsafe.AsRef<short>(&NativePtr->InstanceCurrent);
        public ref short InstanceInteracted => ref Unsafe.AsRef<short>(&NativePtr->InstanceInteracted);
        public ref float RowPosY1 => ref Unsafe.AsRef<float>(&NativePtr->RowPosY1);
        public ref float RowPosY2 => ref Unsafe.AsRef<float>(&NativePtr->RowPosY2);
        public ref float RowMinHeight => ref Unsafe.AsRef<float>(&NativePtr->RowMinHeight);
        public ref float RowTextBaseline => ref Unsafe.AsRef<float>(&NativePtr->RowTextBaseline);
        public ref float RowIndentOffsetX => ref Unsafe.AsRef<float>(&NativePtr->RowIndentOffsetX);
        public ref ImGuiTableRowFlags RowFlags => ref Unsafe.AsRef<ImGuiTableRowFlags>(&NativePtr->RowFlags);
        public ref ImGuiTableRowFlags LastRowFlags => ref Unsafe.AsRef<ImGuiTableRowFlags>(&NativePtr->LastRowFlags);
        public ref int RowBgColorCounter => ref Unsafe.AsRef<int>(&NativePtr->RowBgColorCounter);
        public RangeAccessor<uint> RowBgColor => new RangeAccessor<uint>(NativePtr->RowBgColor, 2);
        public ref uint BorderColorStrong => ref Unsafe.AsRef<uint>(&NativePtr->BorderColorStrong);
        public ref uint BorderColorLight => ref Unsafe.AsRef<uint>(&NativePtr->BorderColorLight);
        public ref float BorderX1 => ref Unsafe.AsRef<float>(&NativePtr->BorderX1);
        public ref float BorderX2 => ref Unsafe.AsRef<float>(&NativePtr->BorderX2);
        public ref float HostIndentX => ref Unsafe.AsRef<float>(&NativePtr->HostIndentX);
        public ref float MinColumnWidth => ref Unsafe.AsRef<float>(&NativePtr->MinColumnWidth);
        public ref float OuterPaddingX => ref Unsafe.AsRef<float>(&NativePtr->OuterPaddingX);
        public ref float CellPaddingX => ref Unsafe.AsRef<float>(&NativePtr->CellPaddingX);
        public ref float CellPaddingY => ref Unsafe.AsRef<float>(&NativePtr->CellPaddingY);
        public ref float CellSpacingX1 => ref Unsafe.AsRef<float>(&NativePtr->CellSpacingX1);
        public ref float CellSpacingX2 => ref Unsafe.AsRef<float>(&NativePtr->CellSpacingX2);
        public ref float LastOuterHeight => ref Unsafe.AsRef<float>(&NativePtr->LastOuterHeight);
        public ref float LastFirstRowHeight => ref Unsafe.AsRef<float>(&NativePtr->LastFirstRowHeight);
        public ref float InnerWidth => ref Unsafe.AsRef<float>(&NativePtr->InnerWidth);
        public ref float ColumnsGivenWidth => ref Unsafe.AsRef<float>(&NativePtr->ColumnsGivenWidth);
        public ref float ColumnsAutoFitWidth => ref Unsafe.AsRef<float>(&NativePtr->ColumnsAutoFitWidth);
        public ref float ResizedColumnNextWidth => ref Unsafe.AsRef<float>(&NativePtr->ResizedColumnNextWidth);
        public ref float ResizeLockMinContentsX2 => ref Unsafe.AsRef<float>(&NativePtr->ResizeLockMinContentsX2);
        public ref float RefScale => ref Unsafe.AsRef<float>(&NativePtr->RefScale);
        public ref ImRect OuterRect => ref Unsafe.AsRef<ImRect>(&NativePtr->OuterRect);
        public ref ImRect InnerRect => ref Unsafe.AsRef<ImRect>(&NativePtr->InnerRect);
        public ref ImRect WorkRect => ref Unsafe.AsRef<ImRect>(&NativePtr->WorkRect);
        public ref ImRect InnerClipRect => ref Unsafe.AsRef<ImRect>(&NativePtr->InnerClipRect);
        public ref ImRect BgClipRect => ref Unsafe.AsRef<ImRect>(&NativePtr->BgClipRect);
        public ref ImRect Bg0ClipRectForDrawCmd => ref Unsafe.AsRef<ImRect>(&NativePtr->Bg0ClipRectForDrawCmd);
        public ref ImRect Bg2ClipRectForDrawCmd => ref Unsafe.AsRef<ImRect>(&NativePtr->Bg2ClipRectForDrawCmd);
        public ref ImRect HostClipRect => ref Unsafe.AsRef<ImRect>(&NativePtr->HostClipRect);
        public ref ImRect HostBackupInnerClipRect => ref Unsafe.AsRef<ImRect>(&NativePtr->HostBackupInnerClipRect);
        public ImGuiWindowPtr OuterWindow => new ImGuiWindowPtr(NativePtr->OuterWindow);
        public ImGuiWindowPtr InnerWindow => new ImGuiWindowPtr(NativePtr->InnerWindow);
        public ref ImGuiTextBuffer ColumnsNames => ref Unsafe.AsRef<ImGuiTextBuffer>(&NativePtr->ColumnsNames);
        public ImDrawListSplitterPtr DrawSplitter => new ImDrawListSplitterPtr(NativePtr->DrawSplitter);
        public ref ImGuiTableColumnSortSpecs SortSpecsSingle => ref Unsafe.AsRef<ImGuiTableColumnSortSpecs>(&NativePtr->SortSpecsSingle);
        public ImPtrVector<ImGuiTableColumnSortSpecsPtr> SortSpecsMulti => new ImPtrVector<ImGuiTableColumnSortSpecsPtr>(NativePtr->SortSpecsMulti, Unsafe.SizeOf<ImGuiTableColumnSortSpecs>());
        public ref ImGuiTableSortSpecs SortSpecs => ref Unsafe.AsRef<ImGuiTableSortSpecs>(&NativePtr->SortSpecs);
        public ref sbyte SortSpecsCount => ref Unsafe.AsRef<sbyte>(&NativePtr->SortSpecsCount);
        public ref sbyte ColumnsEnabledCount => ref Unsafe.AsRef<sbyte>(&NativePtr->ColumnsEnabledCount);
        public ref sbyte ColumnsEnabledFixedCount => ref Unsafe.AsRef<sbyte>(&NativePtr->ColumnsEnabledFixedCount);
        public ref sbyte DeclColumnsCount => ref Unsafe.AsRef<sbyte>(&NativePtr->DeclColumnsCount);
        public ref sbyte HoveredColumnBody => ref Unsafe.AsRef<sbyte>(&NativePtr->HoveredColumnBody);
        public ref sbyte HoveredColumnBorder => ref Unsafe.AsRef<sbyte>(&NativePtr->HoveredColumnBorder);
        public ref sbyte AutoFitSingleColumn => ref Unsafe.AsRef<sbyte>(&NativePtr->AutoFitSingleColumn);
        public ref sbyte ResizedColumn => ref Unsafe.AsRef<sbyte>(&NativePtr->ResizedColumn);
        public ref sbyte LastResizedColumn => ref Unsafe.AsRef<sbyte>(&NativePtr->LastResizedColumn);
        public ref sbyte HeldHeaderColumn => ref Unsafe.AsRef<sbyte>(&NativePtr->HeldHeaderColumn);
        public ref sbyte ReorderColumn => ref Unsafe.AsRef<sbyte>(&NativePtr->ReorderColumn);
        public ref sbyte ReorderColumnDir => ref Unsafe.AsRef<sbyte>(&NativePtr->ReorderColumnDir);
        public ref sbyte LeftMostEnabledColumn => ref Unsafe.AsRef<sbyte>(&NativePtr->LeftMostEnabledColumn);
        public ref sbyte RightMostEnabledColumn => ref Unsafe.AsRef<sbyte>(&NativePtr->RightMostEnabledColumn);
        public ref sbyte LeftMostStretchedColumn => ref Unsafe.AsRef<sbyte>(&NativePtr->LeftMostStretchedColumn);
        public ref sbyte RightMostStretchedColumn => ref Unsafe.AsRef<sbyte>(&NativePtr->RightMostStretchedColumn);
        public ref sbyte ContextPopupColumn => ref Unsafe.AsRef<sbyte>(&NativePtr->ContextPopupColumn);
        public ref sbyte FreezeRowsRequest => ref Unsafe.AsRef<sbyte>(&NativePtr->FreezeRowsRequest);
        public ref sbyte FreezeRowsCount => ref Unsafe.AsRef<sbyte>(&NativePtr->FreezeRowsCount);
        public ref sbyte FreezeColumnsRequest => ref Unsafe.AsRef<sbyte>(&NativePtr->FreezeColumnsRequest);
        public ref sbyte FreezeColumnsCount => ref Unsafe.AsRef<sbyte>(&NativePtr->FreezeColumnsCount);
        public ref sbyte RowCellDataCurrent => ref Unsafe.AsRef<sbyte>(&NativePtr->RowCellDataCurrent);
        public ref byte DummyDrawChannel => ref Unsafe.AsRef<byte>(&NativePtr->DummyDrawChannel);
        public ref byte Bg2DrawChannelCurrent => ref Unsafe.AsRef<byte>(&NativePtr->Bg2DrawChannelCurrent);
        public ref byte Bg2DrawChannelUnfrozen => ref Unsafe.AsRef<byte>(&NativePtr->Bg2DrawChannelUnfrozen);
        public ref bool IsLayoutLocked => ref Unsafe.AsRef<bool>(&NativePtr->IsLayoutLocked);
        public ref bool IsInsideRow => ref Unsafe.AsRef<bool>(&NativePtr->IsInsideRow);
        public ref bool IsInitializing => ref Unsafe.AsRef<bool>(&NativePtr->IsInitializing);
        public ref bool IsSortSpecsDirty => ref Unsafe.AsRef<bool>(&NativePtr->IsSortSpecsDirty);
        public ref bool IsUsingHeaders => ref Unsafe.AsRef<bool>(&NativePtr->IsUsingHeaders);
        public ref bool IsContextPopupOpen => ref Unsafe.AsRef<bool>(&NativePtr->IsContextPopupOpen);
        public ref bool IsSettingsRequestLoad => ref Unsafe.AsRef<bool>(&NativePtr->IsSettingsRequestLoad);
        public ref bool IsSettingsDirty => ref Unsafe.AsRef<bool>(&NativePtr->IsSettingsDirty);
        public ref bool IsDefaultDisplayOrder => ref Unsafe.AsRef<bool>(&NativePtr->IsDefaultDisplayOrder);
        public ref bool IsResetAllRequest => ref Unsafe.AsRef<bool>(&NativePtr->IsResetAllRequest);
        public ref bool IsResetDisplayOrderRequest => ref Unsafe.AsRef<bool>(&NativePtr->IsResetDisplayOrderRequest);
        public ref bool IsUnfrozenRows => ref Unsafe.AsRef<bool>(&NativePtr->IsUnfrozenRows);
        public ref bool IsDefaultSizingPolicy => ref Unsafe.AsRef<bool>(&NativePtr->IsDefaultSizingPolicy);
        public ref bool MemoryCompacted => ref Unsafe.AsRef<bool>(&NativePtr->MemoryCompacted);
        public ref bool HostSkipItems => ref Unsafe.AsRef<bool>(&NativePtr->HostSkipItems);
        public void Destroy()
        {
            ImGuiNative.ImGuiTable_destroy((ImGuiTable*)(NativePtr));
        }
    }
}
