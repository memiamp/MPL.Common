using System;
using System.Runtime.InteropServices;

namespace MPL.Common.Win32.User32
{
    /// <summary>
    /// A class that defines native extern methods for User32.dll.
    /// </summary>
    internal static class NativeMethods
    {
        #region Declarations
        #region _Dll Imports_
        /// <summary>
        /// Sends the specified ListView message.
        /// </summary>
        /// <param name="hWnd">An IntPtr containing the handle to the window.</param>
        /// <param name="msg">An uint indicating the message to send.</param>
        /// <param name="wParam">An UIntPtr containing the pointer to the wParam.</param>
        /// <param name="lParam">An IntPtr containing the pointer to the lParam.</param>
        /// <returns>An IntPtr containing the result.</returns>
        [DllImport("user32.dll", SetLastError = true, EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessageListView(IntPtr hWnd, uint msg, UIntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Sends the specified Windows Message.
        /// </summary>
        /// <param name="hWnd">An IntPtr containing the handle to the window.</param>
        /// <param name="msg">An int indicating the message to send.</param>
        /// <param name="wParam">An int containing the pointer to the wParam.</param>
        /// <param name="lParam">An int containing the pointer to the lParam.</param>
        /// <returns>An IntPtr containing the result.</returns>
        [DllImport("user32.dll", SetLastError = true, EntryPoint = "SendMessage")]
        private static extern IntPtr SendMessageWindowsMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        #endregion
        #endregion

        #region Methods
        #region _Internal_
        /// <summary>
        /// Sends the specified ListViewMessage.
        /// </summary>
        /// <param name="hWnd">An IntPtr containing the handle to the ListView.</param>
        /// <param name="msg">A ListViewMessage indicating the message to send.</param>
        /// <param name="wParam">An UIntPtr containing the pointer to the wParam.</param>
        /// <param name="lParam">An IntPtr containing the pointer to the lParam.</param>
        /// <returns>An IntPtr containing the result.</returns>
        internal static IntPtr SendMessage(IntPtr hWnd, ListViewMessage msg, UIntPtr wParam, IntPtr lParam)
        {
            return SendMessageListView(hWnd, (uint)msg, wParam, lParam);
        }

        /// <summary>
        /// Sends the specified Windows Message.
        /// </summary>
        /// <param name="hWnd">An IntPtr containing the handle to the ListView.</param>
        /// <param name="msg">A WindowsMessage indicating the message to send.</param>
        /// <param name="wParam">An int containing the value of the wParam.</param>
        /// <param name="lParam">An int containing the value of the lParam.</param>
        /// <returns>An IntPtr containing the result.</returns>
        internal static IntPtr SendMessage(IntPtr hWnd, WindowsMessage msg, int wParam, int lParam)
        {
            return SendMessageWindowsMessage(hWnd, (int)msg, new IntPtr(wParam), new IntPtr(lParam));
        }

        #endregion
        #endregion
    }
}