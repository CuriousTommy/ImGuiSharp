﻿using System;
using System.Runtime.InteropServices;

namespace ImGuiSharp
{
    public struct ImGuiStoragePair
    {
        public uint Key;
        public UnionValue Value;
    }

    public unsafe struct ImGuiStoragePairPtr
    {
        public ImGuiStoragePair* NativePtr { get; }
        public ImGuiStoragePairPtr(ImGuiStoragePair*                          nativePtr) => NativePtr = nativePtr;
        public ImGuiStoragePairPtr(IntPtr                                     nativePtr) => NativePtr = (ImGuiStoragePair*)nativePtr;
        public static implicit operator ImGuiStoragePairPtr(ImGuiStoragePair* nativePtr)  => new(nativePtr);
        public static implicit operator ImGuiStoragePair*(ImGuiStoragePairPtr wrappedPtr) => wrappedPtr.NativePtr;
        public static implicit operator ImGuiStoragePairPtr(IntPtr            nativePtr)  => new(nativePtr);
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct UnionValue
    {
        [FieldOffset(0)]
        public int ValueI32;
        [FieldOffset(0)]
        public float ValueF32;
        [FieldOffset(0)]
        public IntPtr ValuePtr;
    }
}
