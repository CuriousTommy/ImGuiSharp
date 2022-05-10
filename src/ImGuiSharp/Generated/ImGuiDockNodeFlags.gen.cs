// ReSharper disable once CheckNamespace
namespace ImGuiSharp
{
    [System.Flags]
    public enum ImGuiDockNodeFlags
    {
        None = 0,
        KeepAliveOnly = 1,
        NoDockingInCentralNode = 4,
        PassthruCentralNode = 8,
        NoSplit = 16,
        NoResize = 32,
        AutoHideTabBar = 64,

        // Manually added entries from ImGuiDockNodeFlagsPrivate_
        DockSpace = 1024 /* 1 << 10 */,
        CentralNode = 2048 /* 1 << 11 */,
        NoTabBar = 4096 /* 1 << 12 */,
        HiddenTabBar = 8192 /* 1 << 13 */,
        NoWindowMenuButton = 16384 /* 1 << 14 */,
        NoCloseButton = 32768 /* 1 << 15 */,
        NoDocking = 65536 /* 1 << 16 */,
        NoDockingSplitMe = 131072 /* 1 << 17 */,
        NoDockingSplitOther = 262144 /* 1 << 18 */,
        NoDockingOverMe = 524288 /* 1 << 19 */,
        NoDockingOverOther = 1048576 /* 1 << 20 */,
        NoDockingOverEmpty = 2097152 /* 1 << 21 */,
        NoResizeX = 4194304 /* 1 << 22 */,
        NoResizeY = 8388608 /* 1 << 23 */,
    }
}
