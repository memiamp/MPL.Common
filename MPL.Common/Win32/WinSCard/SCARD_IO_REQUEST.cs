using System;
using System.Runtime.InteropServices;

namespace MPL.Common.Win32.WinSCard
{
    /// <summary>
    /// A structure that defines the beginning of a protocol control information structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct SCARD_IO_REQUEST
    {
        public SCardProtocol dwProtocol;
        public uint cbPciLength;
    }
}