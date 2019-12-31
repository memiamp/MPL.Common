using System;
using System.Runtime.InteropServices;

namespace MPL.Common.Win32.HttpApi
{
    /// <summary>
    /// A class that defines native extern methods for the HttpApi.
    /// </summary>
    internal static class NativeMethods
    {
        #region Declarations
        #region _Dll Imports_
        [DllImport("httpapi.dll", SetLastError = true)]
        internal static extern uint HttpDeleteServiceConfiguration(IntPtr ServiceIntPtr, HTTP_SERVICE_CONFIG_ID ConfigId, IntPtr pConfigInformation, int ConfigInformationLength, IntPtr pOverlapped);

        [DllImport("httpapi.dll", SetLastError = true)]
        internal static extern uint HttpInitialize(HTTPAPI_VERSION Version, uint Flags, IntPtr pReserved);

        [DllImport("httpapi.dll", SetLastError = true)]
        internal static extern uint HttpSetServiceConfiguration(IntPtr ServiceIntPtr, HTTP_SERVICE_CONFIG_ID ConfigId, IntPtr pConfigInformation, int ConfigInformationLength, IntPtr pOverlapped);

        [DllImport("httpapi.dll", SetLastError = true)]
        internal static extern uint HttpTerminate(uint Flags, IntPtr pReserved);

        #endregion
        #endregion
    }
}