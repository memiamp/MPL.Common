using System;
using System.Runtime.InteropServices;

namespace MPL.Common.Win32.User32
{
    /// <summary>
    /// A struct that defines the user32 type LVGROUP.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct LVGROUP
    {
        public uint cbSize;
        public ListViewGroupFlags mask;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pszHeader;
        public int cchHeader;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pszFooter;
        public int cchFooter;
        public int iGroupId;
        public uint stateMask;
        public ListViewGroupStyle state;
        public uint uAlign;
    }
}