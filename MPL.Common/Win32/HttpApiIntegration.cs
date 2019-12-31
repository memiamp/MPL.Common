using MPL.Common.Win32.HttpApi;
using System;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace MPL.Common.Win32
{
    /// <summary>
    /// A class that provides integration with the HTTP API.
    /// </summary>
    public static class HttpApiIntegration
    {
        #region Declarations
        #region _Consts_
        private const uint HTTP_INITIALIZE_CONFIG = 0x00000002;

        private const string cSDDL_STANDARD_FORMAT = "D:(A;;GX;;;{0})";

        #endregion
        #endregion

        #region Methods
        #region _Private_
        private static string CreateSddlFromSid(string sid)
        {
            return string.Format(cSDDL_STANDARD_FORMAT, sid);
        }

        private static bool CreateSddlFromUserAccount(string userAccount, out string sddl)
        {
            bool ReturnValue = false;

            // Defaults
            sddl = null;

            if (GetSID(userAccount, out string SID))
            {
                sddl = CreateSddlFromSid(SID);
                ReturnValue = true;
            }

            return ReturnValue;
        }

        private static HTTP_SERVICE_CONFIG_URLACL_SET CreateUrlAclSet(string key, string param)
        {
            HTTP_SERVICE_CONFIG_URLACL_SET ReturnValue;

            ReturnValue = new HTTP_SERVICE_CONFIG_URLACL_SET()
            {
                KeyDesc = new HTTP_SERVICE_CONFIG_URLACL_KEY(key),
                ParamDesc = new HTTP_SERVICE_CONFIG_URLACL_PARAM(param)
            };

            return ReturnValue;
        }

        private static bool CreateUrlAclSetFromUserAccount(string key, string userAccount, out HTTP_SERVICE_CONFIG_URLACL_SET urlAclSet)
        {
            bool ReturnValue = false;

            // Defaults
            urlAclSet = default(HTTP_SERVICE_CONFIG_URLACL_SET);

            if (CreateSddlFromUserAccount(userAccount, out string SecurityDescriptor))
            {
                urlAclSet = CreateUrlAclSet(key, SecurityDescriptor);
                ReturnValue = true;
            }

            return ReturnValue;
        }

        private static bool GetSID(string userAccount, out string sid)
        {
            bool ReturnValue = false;

            // Defaults
            sid = null;

            try
            {
                NTAccount Account;
                SecurityIdentifier SID;

                if (userAccount.Contains(@"\"))
                    userAccount = userAccount.Substring(userAccount.IndexOf(@"\") + 1);
                Account = new NTAccount(userAccount);
                SID = (SecurityIdentifier)Account.Translate(typeof(SecurityIdentifier));
                sid = SID.ToString();
                ReturnValue = true;
            }
            catch (Exception)
            {
                ReturnValue = false;
            }

            return ReturnValue;
        }

        private static bool InitialiseHttpApi()
        {
            uint Result;
            bool ReturnValue = false;

            Result = NativeMethods.HttpInitialize(new HTTPAPI_VERSION(1, 0), HTTP_INITIALIZE_CONFIG, IntPtr.Zero);
            if (Result == 0)
                ReturnValue = true;

            return ReturnValue;
        }

        private static bool TerminateHttpApi()
        {
            uint Result;
            bool ReturnValue = false;

            Result = NativeMethods.HttpTerminate(HTTP_INITIALIZE_CONFIG, IntPtr.Zero);
            if (Result == 0)
                ReturnValue = true;

            return ReturnValue;
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Adds a URL ACL exception via the HTTP API for the specified user account.
        /// </summary>
        /// <param name="url">A string containing the URL to add an exception for.</param>
        /// <param name="userAccount">A string containing the username of the user to add the exception for.</param>
        /// <param name="consoleOutput">An IConsoleOutputWrapper that provides a console for output. If NULL, then the default System.Console will be used.</param>
        /// <returns>A bool that indicates whether the exception was added successfully.</returns>
        public static bool AddUrlException(string url, string userAccount, IConsoleOutputWrapper consoleOutput = null)
        {
            bool ReturnValue = false;

            if (consoleOutput == null)
                consoleOutput = new ConsoleOutput();

            InitialiseHttpApi();

            if (CreateUrlAclSetFromUserAccount(url, userAccount, out HTTP_SERVICE_CONFIG_URLACL_SET UrlAclSet))
            {
                bool ShouldRetry = true;
                uint Result;
                int RetryCount = 0;
                IntPtr UrlAclSetPointer;

                consoleOutput.WriteLine("Set configuration for '{0}'", UrlAclSet.KeyDesc.pUrlPrefix);
                consoleOutput.WriteLine("SDDL is '{0}'", UrlAclSet.ParamDesc.pStringSecurityDescriptor);
                consoleOutput.WriteLine();

                UrlAclSetPointer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(HTTP_SERVICE_CONFIG_URLACL_SET)));
                Marshal.StructureToPtr(UrlAclSet, UrlAclSetPointer, false);

                while (ShouldRetry && RetryCount++ < 4)
                {
                    ShouldRetry = false;

                    // Attempt to set the configuration and process the result
                    consoleOutput.Write("Attempting {0}: ", RetryCount);
                    Result = NativeMethods.HttpSetServiceConfiguration(IntPtr.Zero, HTTP_SERVICE_CONFIG_ID.HttpServiceConfigUrlAclInfo, UrlAclSetPointer, Marshal.SizeOf(typeof(HTTP_SERVICE_CONFIG_URLACL_SET)), IntPtr.Zero);
                    switch (Result)
                    {
                        case 0:
                            consoleOutput.WriteLine("Success.");
                            ReturnValue = true;
                            break;

                        case 183: // Entry already exists
                            // Attempt to remove the existing configuration and retry
                            consoleOutput.WriteLine("Service configuration already exists. The existing configuration will be removed.");
                            RemoveUrlException(url);
                            ShouldRetry = true;
                            break;

                        default:
                            consoleOutput.WriteLine("Error {0} returned.", Result);
                            break;
                    }

                    consoleOutput.WriteLine();
                }
            }

            TerminateHttpApi();

            return ReturnValue;
        }

        /// <summary>
        /// Removes a URL ACL exception via the HTTP API.
        /// </summary>
        /// <param name="url">A string containing the URL to remove.</param>
        /// <param name="consoleOutput">An IConsoleOutputWrapper that provides a console for output. If NULL, then the default System.Console will be used.</param>
        /// <returns>A bool that indicates whether the exception was removed successfully.</returns>
        public static bool RemoveUrlException(string url, IConsoleOutputWrapper consoleOutput = null)
        {
            uint Result;
            bool ReturnValue = false;
            IntPtr UrlAclSetPointer;

            if (consoleOutput == null)
                consoleOutput = new ConsoleOutput();

            InitialiseHttpApi();

            HTTP_SERVICE_CONFIG_URLACL_SET UrlAclSet = new HTTP_SERVICE_CONFIG_URLACL_SET()
            {
                KeyDesc = new HTTP_SERVICE_CONFIG_URLACL_KEY(url),
                ParamDesc = new HTTP_SERVICE_CONFIG_URLACL_PARAM("")
            };

            consoleOutput.WriteLine("Delete configuration for '{0}'", UrlAclSet.KeyDesc.pUrlPrefix);
            UrlAclSetPointer = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(HTTP_SERVICE_CONFIG_URLACL_SET)));
            Marshal.StructureToPtr(UrlAclSet, UrlAclSetPointer, false);

            // Attempt to set the configuration and process the result
            Result = NativeMethods.HttpDeleteServiceConfiguration(IntPtr.Zero, HTTP_SERVICE_CONFIG_ID.HttpServiceConfigUrlAclInfo, UrlAclSetPointer, Marshal.SizeOf(typeof(HTTP_SERVICE_CONFIG_URLACL_SET)), IntPtr.Zero);
            switch (Result)
            {
                case 0:
                    consoleOutput.WriteLine("Success.");
                    ReturnValue = true;
                    break;

                default:
                    consoleOutput.WriteLine("Error {0} returned.", Result);
                    break;
            }

            TerminateHttpApi();

            consoleOutput.WriteLine();

            return ReturnValue;
        }

        #endregion
        #endregion
    }
}