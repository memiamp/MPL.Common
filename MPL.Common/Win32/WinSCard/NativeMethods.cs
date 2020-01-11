using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace MPL.Common.Win32.WinSCard
{
    /// <summary>
    /// A class that defines native extern methods for WinSCard.dll.
    /// </summary>
    internal static class NativeMethods
    {
        #region Declarations
        #region _Dll Imports_
        /// <summary>
        /// Establish the context within which winscard operations are performed.
        /// </summary>
        /// <param name="dwScope">A SCardContextScope indicating the scope of the resource manager context.</param>
        /// <param name="pvReserved1">An IntPtr that is reserved for future use.</param>
        /// <param name="pvReserved2">An IntPtr that is reserved for future use.</param>
        /// <param name="phContext">An IntPtr that will be set to the handle established by the resource manager context.</param>
        /// <returns>An uint indicating the result of the operation.</returns>
        [DllImport("winscard.dll")]
        private static extern uint SCardEstablishContext(SCardContextScope dwScope, IntPtr pvReserved1, IntPtr pvReserved2, out IntPtr phContext);

        /// <summary>
        /// Makes a blocking call that completes once any monitored card changes.
        /// </summary>
        /// <param name="hContext">An IntPtr that is the established resource manager context to use.</param>
        /// <param name="dwTimeout">An uint that is the maximum amount of time toi wait for a change, in milliseconds.</param>
        /// <param name="rgReaderState">An array of SCARD_READERSTATE that specified the readers to watch.</param>
        /// <param name="cReaders">An uint indicating the number of readers in the rgReaderState array.</param>
        /// <returns>An uint indicating the result of the operation.</returns>
        [DllImport("winscard.dll", CharSet = CharSet.Unicode)]
        private static extern uint SCardGetStatusChange(IntPtr hContext, uint dwTimeout, [In, Out] SCARD_READERSTATE[] rgReaderState, uint cReaders);

        /// <summary>
        /// Gets a list of readers within a set name of reader groups.
        /// </summary>
        /// <param name="hContext">An IntPtr that is the established resource manager context to use.</param>
        /// <param name="mszGroups">A string that contains a multi-string that defines the reader groups to list.</param>
        /// <param name="mszReaders">A string that contains a multi-string that defines the card readers to list.</param>
        /// <param name="pcchReaders">The length of the mszReaders buffer in characters.</param>
        /// <returns>An uint indicating the result of the operation.</returns>
        [DllImport("winscard.dll", CharSet = CharSet.Unicode)]
        private static extern uint SCardListReaders(IntPtr hContext, [MarshalAs(UnmanagedType.LPWStr)] string mszGroups, [MarshalAs(UnmanagedType.LPWStr)]string mszReaders, ref uint pcchReaders);

        /// <summary>
        /// Closes the specified contact and releases associated resources.
        /// </summary>
        /// <param name="hContext">An IntPtr that is the established resource manager context to release.</param>
        /// <returns>An uint indicating the result of the operation.</returns>
        [DllImport("winscard.dll")]
        private static extern uint SCardReleaseContext(IntPtr hContext);

        #endregion
        #endregion

        #region Methods
        #region _Internal_
        /// <summary>
        /// Establishes a context for winscard operations.
        /// </summary>
        /// <param name="context">An IntPtr that will be set to the context.</param>
        /// <param name="result">A SCardResponse that will be set to the result of the operation.</param>
        /// <returns>A bool that indicates whether the operation was a success.</returns>
        internal static bool EstablishContext(out IntPtr context, out SCardResponse result)
        {
            uint resultCode;
            bool returnValue = false;

            resultCode = SCardEstablishContext(SCardContextScope.SCOPE_SYSTEM, IntPtr.Zero, IntPtr.Zero, out context);
            result = (SCardResponse)resultCode;
            if (result == SCardResponse.SCARD_S_SUCCESS)
                returnValue = true;

            return returnValue;
        }

        /// <summary>
        /// Gets the status of the specified reader states, blocking the current thread until the specified timeout.
        /// </summary>
        /// <param name="context">An IntPtr that is the context to use.</param>
        /// <param name="readerStates">An array of SCARD_READERSTATE that are the readers to watch.</param>
        /// <param name="timeout">An uint indicating the timeout period in milliseconds.</param>
        /// <param name="result">A SCardResponse that will bes et to the result of the operation.</param>
        /// <returns>A bool that indicates whether the operation was a success.</returns>
        internal static bool GetStatusChange(IntPtr context, SCARD_READERSTATE[] readerStates, uint timeout, out SCardResponse result)
        {
            uint resultCode;
            bool returnValue = false;

            resultCode = SCardGetStatusChange(context, timeout, readerStates, (uint)readerStates.Length);
            result = (SCardResponse)resultCode;
            if (result == SCardResponse.SCARD_S_SUCCESS)
                returnValue = true;

            return returnValue;

        }

        /// <summary>
        /// Lists readers in the specified group using the specified context.
        /// </summary>
        /// <param name="context">An IntPtr that is the context to use.</param>
        /// <param name="group">A SCardReaderGroup that is the reader group to use.</param>
        /// <param name="readers">An array of string that will set to the list of readers.</param>
        /// <param name="result">A SCardResponse that will bes et to the result of the operation.</param>
        /// <returns>A bool that indicates whether the operation was a success.</returns>
        internal static bool ListReaders(IntPtr context, SCardReaderGroup group, out string[] readers, out SCardResponse result)
        {
            uint readerSize = 0;
            uint resultCode;
            bool returnValue = false;

            // Defaults
            readers = null;

            resultCode = SCardListReaders(context, GetGroupName(group), null, ref readerSize);
            result = (SCardResponse)resultCode;
            if (result == SCardResponse.SCARD_S_SUCCESS)
            {
                string readerBuffer;

                readerBuffer = "".PadLeft((int)readerSize);
                resultCode = SCardListReaders(context, GetGroupName(group), readerBuffer, ref readerSize);
                result = (SCardResponse)resultCode;
                if (result == SCardResponse.SCARD_S_SUCCESS)
                {
                    readers = readerBuffer.Split('\0').Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim()).ToArray();
                    returnValue = true;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Releases a context from winscard operations.
        /// </summary>
        /// <param name="context">An IntPtr that is the context to release.</param>
        /// <param name="result">A SCardResponse that will be set to the result of the operation.</param>
        /// <returns>A bool that indicates whether the operation was a success.</returns>
        internal static bool ReleaseContext(IntPtr context, out SCardResponse result)
        {
            bool returnValue = false;

            // Defaults
            result = SCardResponse.SCARD_E_INVALID_HANDLE;

            // Make sure there is a valid context to release
            if (context != IntPtr.Zero)
            {
                uint resultCode;

                resultCode = SCardReleaseContext(context);
                result = (SCardResponse)resultCode;
                if (result == SCardResponse.SCARD_S_SUCCESS)
                    returnValue = true;
            }

            return returnValue;
        }

        #endregion
        #region _Private_
        private static string GetGroupName(SCardReaderGroup group)
        {
            string returnValue;

            switch (group)
            {
                case SCardReaderGroup.AllReaders:
                    returnValue = "SCard$AllReaders";
                    break;

                case SCardReaderGroup.DefaultReaders:
                    returnValue = "SCard$DefaultReaders";
                    break;

                case SCardReaderGroup.LocalReaders:
                    returnValue = "SCard$LocalReaders";
                    break;

                case SCardReaderGroup.SystemReaders:
                    returnValue = "SCard$SystemReaders";
                    break;

                default:
                    throw new ArgumentException($"The group {group} was not recognised", nameof(group));
            }

            return returnValue;
        }

        #endregion
        #endregion
    }
}