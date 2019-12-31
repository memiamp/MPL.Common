using System;
using System.Runtime.InteropServices;

namespace MPL.Common.Win32.HttpApi
{
    /// <summary>
    /// A struct that defines the HttpApi type HTTPAPI_VERSION.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal struct HTTPAPI_VERSION
    {
        public ushort HttpApiMajorVersion;
        public ushort HttpApiMinorVersion;

        public HTTPAPI_VERSION(ushort majorVersion, ushort minorVersion)
        {
            HttpApiMajorVersion = majorVersion;
            HttpApiMinorVersion = minorVersion;
        }
    }
}