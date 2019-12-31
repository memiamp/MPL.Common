using System;
using System.Runtime.InteropServices;

namespace MPL.Common.Win32.HttpApi
{
    /// <summary>
    /// A struct that defines the HttpApi type HTTP_SERVICE_CONFIG_URLACL_KEY.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct HTTP_SERVICE_CONFIG_URLACL_KEY
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pUrlPrefix;

        public HTTP_SERVICE_CONFIG_URLACL_KEY(string urlPrefix)
        {
            pUrlPrefix = urlPrefix;
        }
    }
}