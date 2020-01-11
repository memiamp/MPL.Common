using System;
using System.Runtime.InteropServices;

namespace MPL.Common.Win32.WinSCard
{
    /// <summary>
    /// A struct that defines the windscard type SCARD_READERSTATE.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct SCARD_READERSTATE
    {
        public string szReader;
        public IntPtr pvUserData;
        public SCardReaderEventState dwCurrentState;
        public uint dwEventState;
        public uint cbAtr;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x24, ArraySubType = UnmanagedType.U1)]
        public byte[] rgbAtr;
    }
}