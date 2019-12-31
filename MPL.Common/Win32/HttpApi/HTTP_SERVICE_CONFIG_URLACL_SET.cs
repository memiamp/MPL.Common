using System;
using System.Runtime.InteropServices;

namespace MPL.Common.Win32.HttpApi
{
    /// <summary>
    /// A struct that defines the HttpApi type HTTP_SERVICE_CONFIG_URLACL_SET.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct HTTP_SERVICE_CONFIG_URLACL_SET
    {
        public HTTP_SERVICE_CONFIG_URLACL_KEY KeyDesc;
        public HTTP_SERVICE_CONFIG_URLACL_PARAM ParamDesc;
    }
}