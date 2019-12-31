using System;

namespace MPL.Common.Win32.User32
{
    #region Declarations
    #region _Enumerations_
    /// <summary>
    /// An enumeration that defines ListView Group Flags.
    /// </summary>
    [Flags()]
    internal enum ListViewGroupFlags : uint
    {
        ALIGN = 0x0008,

        DESCRIPTIONBOTTOM = 0x800,

        DESCRIPTIONTOP = 0x400,

        EXTENDEDIMAGE = 0x2000,

        FOOTER = 0x0002,

        GROUPID = 0x0010,

        HEADER = 0x0001,

        ITEMS = 0x4000,

        STATE = 0x0004,

        SUBSET = 0x8000,

        SUBSETITEMS = 0x10000,

        SUBTITLE = 0x100,

        TASK = 0x200,

        TITLEIMAGE = 0x1000
    }

    /// <summary>
    /// An enumeration that defines ListView Group States.
    /// </summary>
    [Flags()]
    internal enum ListViewGroupStyle : uint
    {
        COLLAPSED = 1,

        COLLAPSIBLE = 8,

        FOCUSED = 16,

        HIDDEN = 2,

        NOHEADER = 4,

        NORMAL = 0,

        SELECTED = 32,

        SUBSETED = 64,

        SUBSETLINKFOCUSED = 128
    }

    /// <summary>
    /// An enumeration that defines a ListView message.
    /// </summary>
    internal enum ListViewMessage : uint
    {
        LVM_FIRST = 0x1000,
        LVM_GETITEMCOUNT = LVM_FIRST + 4,
        LVM_GETNEXTITEM = LVM_FIRST + 12,
        LVM_GETITEMRECT = LVM_FIRST + 14,
        LVM_GETITEMPOSITION = LVM_FIRST + 16,
        LVM_HITTEST = (LVM_FIRST + 18),
        LVM_ENSUREVISIBLE = LVM_FIRST + 19,
        LVM_SCROLL = LVM_FIRST + 20,
        LVM_GETHEADER = LVM_FIRST + 31,
        LVM_GETITEMSTATE = LVM_FIRST + 44,
        LVM_SETITEMSTATE = LVM_FIRST + 43,
        LVM_GETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 55,
        LVM_GETSUBITEMRECT = LVM_FIRST + 56,
        LVM_SUBITEMHITTEST = LVM_FIRST + 57,
        LVM_APPROXIMATEVIEWRECT = LVM_FIRST + 64,
        LVM_GETITEMW = LVM_FIRST + 75,
        LVM_GETTOOLTIPS = LVM_FIRST + 78,
        LVM_GETFOCUSEDGROUP = LVM_FIRST + 93,
        LVM_GETGROUPRECT = LVM_FIRST + 98,
        LVM_EDITLABEL = LVM_FIRST + 118,
        LVM_GETVIEW = LVM_FIRST + 143,
        LVM_SETVIEW = LVM_FIRST + 142,
        LVM_SETGROUPINFO = LVM_FIRST + 147,
        LVM_GETGROUPINFO = LVM_FIRST + 149,
        LVM_GETGROUPINFOBYINDEX = LVM_FIRST + 153,
        LVM_GETGROUPMETRICS = LVM_FIRST + 156,
        LVM_HASGROUP = LVM_FIRST + 161,
        LVM_ISGROUPVIEWENABLED = LVM_FIRST + 175,
        LVM_GETFOCUSEDCOLUMN = LVM_FIRST + 186,
        LVM_GETEMPTYTEXT = LVM_FIRST + 204,
        LVM_GETFOOTERRECT = LVM_FIRST + 205,
        LVM_GETFOOTERINFO = LVM_FIRST + 206,
        LVM_GETFOOTERITEMRECT = LVM_FIRST + 207,
        LVM_GETFOOTERITEM = LVM_FIRST + 208,
        LVM_GETITEMINDEXRECT = LVM_FIRST + 209,
        LVM_SETITEMINDEXSTATE = LVM_FIRST + 210,
        LVM_GETNEXTITEMINDEX = LVM_FIRST + 211
    }

    /// <summary>
    /// An enumeration that defines a Windows Message.
    /// </summary>
    internal enum WindowsMessage : int
    {
        WM_SETREDRAW = 11
    }

    #endregion
    #endregion
}