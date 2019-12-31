using System;

namespace MPL.Common.Win32.HttpApi
{
    #region Declarations
    #region _Enumerations_
    /// <summary>
    /// An enumeration that defines a HTTP_SERVICE_CONFIG_ID.
    /// </summary>
    internal enum HTTP_SERVICE_CONFIG_ID
    {
        HttpServiceConfigIPListenList = 0,
        HttpServiceConfigSSLCertInfo = 1,
        HttpServiceConfigUrlAclInfo = 2,
        HttpServiceConfigMax = 3
    }

    #endregion
    #endregion
}