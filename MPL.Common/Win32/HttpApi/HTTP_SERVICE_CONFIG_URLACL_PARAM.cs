using System;
using System.Runtime.InteropServices;

namespace MPL.Common.Win32.HttpApi
{
    /// <summary>
    /// A struct that defines the HttpApi type HTTP_SERVICE_CONFIG_URLACL_PARAM.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct HTTP_SERVICE_CONFIG_URLACL_PARAM
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pStringSecurityDescriptor;

        public HTTP_SERVICE_CONFIG_URLACL_PARAM(string securityDescriptor)
        {
            pStringSecurityDescriptor = securityDescriptor;
        }
    }
}