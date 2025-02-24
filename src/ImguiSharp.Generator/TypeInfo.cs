namespace ImGuiSharp.Generator;

public static class TypeInfo
{
    public static readonly Dictionary<string, string> Types = new()
    {
        {"bool", "byte"},
        {"unsigned char", "byte"},
        {"signed char", "sbyte"},
        {"char", "byte"},
        {"ImWchar", "ushort"},
        {"ImFileHandle", "IntPtr"},
        {"ImU8", "byte"},
        {"ImS8", "sbyte"},
        {"ImU16", "ushort"},
        {"ImS16", "short"},
        {"ImU32", "uint"},
        {"ImS32", "int"},
        {"ImU64", "ulong"},
        {"ImS64", "long"},
        {"unsigned short", "ushort"},
        {"unsigned int", "uint"},
        {"ImVec2", "Vector2"},
        {"ImVec2_Simple", "Vector2"},
        {"ImVec3", "Vector3"},
        {"ImVec4", "Vector4"},
        {"ImWchar16", "ushort"}, //char is not blittable
        {"ImVec4_Simple", "Vector4"},
        {"ImColor_Simple", "ImColor"},
        {"ImTextureID", "IntPtr"},
        {"ImGuiID", "uint"},
        {"ImDrawIdx", "ushort"},
        {"ImDrawListSharedData", "IntPtr"},
        {"ImDrawListSharedData*", "IntPtr"},
        {"ImDrawCallback", "IntPtr"},
        {"size_t", "uint"},
        {"ImGuiContext*", "IntPtr"},
        {"ImPlotContext*", "IntPtr"},
        {"EditorContext*", "IntPtr"},
        {"ImGuiMemAllocFunc", "IntPtr"},
        {"ImGuiMemFreeFunc", "IntPtr"},
        {"ImFontBuilderIO", "IntPtr"},
        {"float[2]", "Vector2*"},
        {"float[3]", "Vector3*"},
        {"float[4]", "Vector4*"},
        {"int[2]", "int*"},
        {"int[3]", "int*"},
        {"int[4]", "int*"},
        {"float&", "float*"},
        {"ImVec2[2]", "Vector2*"},
        {"char* []", "byte**"},
        {"unsigned char[256]", "byte*"},
        {"char[5]", "byte*"}, //TODO REMOVE
        {"ImGuiTableColumnIdx", "sbyte"},
        {"ImGuiDockRequest", "IntPtr"},
        {"ImGuiDockNodeSettings", "IntPtr"},
        {"ImGuiStoragePair", "IntPtr"},
        {"ImGuiTableDrawChannelIdx", "byte"},
        {"ImGuiSizeCallback", "IntPtr"},
        {"ImBitArrayForNamedKeys", "IntPtr"},   //TODO
        {"ImSpan_ImGuiTableColumn", "byte"},    //TODO
        {"ImSpan_ImGuiTableColumnIdx", "byte"}, //TODO
        {"ImSpan_ImGuiTableCellData", "byte"},  //TODO
        {"ImPool_ImGuiTable", "IntPtr"},        //TODO
        {"ImPool_ImGuiTabBar", "IntPtr"},       //TODO
        {"ImGuiContextHookCallback", "IntPtr"}, //TODO
        {"ImGuiErrorLogCallback", "IntPtr"},    //TODO
        {"ImVector_ImU32", "ImVector"},          //TODO 1
    };

    public static readonly List<string> WellKnownEnums = new()
    {
        "ImGuiMouseButton"
    };

    public static readonly HashSet<string> CustomDefinedTypes = new()
    {
        "ImVector",
        "ImVec2",
        "ImVec4",
        "ImGuiStoragePair"
    };

    public static readonly Dictionary<string, string> DefaultValues = new()
    {
        {"((void *)0)", "null"},
        {"((void*)0)", "null"},
        {"NULL", "null"},
        {"nullptr", "null"},
        {"ImVec2(0,0)", "new Vector2()"},
        {"ImVec2(0.0f,0.0f)", "new Vector2()"},
        {"ImVec2(-FLT_MIN,0)", "new Vector2(-float.MinValue, 0.0f)"},
        {"ImVec2(-1,0)", "new Vector2(-1, 0)"},
        {"ImVec2(1,0)", "new Vector2(1, 0)"},
        {"ImVec2(1,1)", "new Vector2(1, 1)"},
        {"ImVec2(0,1)", "new Vector2(0, 1)"},
        {"ImVec4(0,0,0,0)", "new Vector4()"},
        {"ImVec4(1,1,1,1)", "new Vector4(1, 1, 1, 1)"},
        {"ImVec4(0,0,0,-1)", "new Vector4(0, 0, 0, -1)"},
        {"ImPlotPoint(0,0)", "new ImPlotPoint { x = 0, y = 0 }"},
        {"ImPlotPoint(1,1)", "new ImPlotPoint { x = 1, y = 1 }"},
        {"ImDrawCornerFlags_All", "ImDrawCornerFlags.All"},
        {"ImPlotFlags_None", "ImPlotFlags.None"},
        {"ImPlotAxisFlags_None", "ImPlotAxisFlags.None"},
        {"ImPlotAxisFlags_NoGridLines", "ImPlotAxisFlags.NoGridLines"},
        {"ImGuiCond_Once", "ImGuiCond.Once"},
        {"ImPlotOrientation_Vertical", "ImPlotOrientation.Vertical"},
        {"PinShape_CircleFilled", "PinShape.CircleFilled"},
        {"ImGuiPopupFlags_None", "ImGuiPopupFlags.None"},
        {"ImGuiNavHighlightFlags_TypeDefault", "ImGuiNavHighlightFlags.TypeDefault"},
        {"ImGuiKeyModFlags_Ctrl", "ImGuiKeyModFlags.Ctrl"},
        {"ImPlotYAxis_1", "ImPlotYAxis._1"},
        {"FLT_MAX", "float.MaxValue"},
        {"(((ImU32)(255)<<24)|((ImU32)(255)<<16)|((ImU32)(255)<<8)|((ImU32)(255)<<0))", "0xFFFFFFFF"},
        {"sizeof(ImU8)", "sizeof(byte)"},
        {"sizeof(ImS8)", "sizeof(sbyte)"},
        {"sizeof(ImU16)", "sizeof(ushort)"},
        {"sizeof(ImS16)", "sizeof(short)"},
        {"sizeof(ImU32)", "sizeof(uint)"},
        {"sizeof(ImS32)", "sizeof(int)"},
        {"sizeof(ImU64)", "sizeof(ulong)"},
        {"sizeof(ImS64)", "sizeof(long)"}
    };

    public static readonly Dictionary<string, string> IdentifierReplacements = new()
    {
        {"in", "@in"},
        {"out", "@out"},
        {"ref", "@ref"}
    };

    public static readonly HashSet<string> LegalFixedTypes = new()
    {
        "byte",
        "sbyte",
        "char",
        "ushort",
        "short",
        "uint",
        "int",
        "ulong",
        "long",
        "float",
        "double"
    };

    public static readonly HashSet<string> SkippedFunctions = new()
    {
        "igInputText",
        "igInputTextMultiline",
        "igCalcTextSize",
        "igInputTextWithHint"
    };

    public static readonly List<string> IgnoredEnums = new()
    {
        "ImGuiKeyPrivate"
    };
    
    /// <summary>
    /// Used in the GetWrappedType()
    /// </summary>
    public static readonly List<string> TypesToNotToWrap = new()
    {
        "ImVector",
        "ImChunkStream"
    };

    public static readonly List<string> PrefixToRemove = new()
    {
        "ImVector_",
        "ImChunkStream_"
    };
}