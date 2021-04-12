using MPL.Common.Win32.User32;
using System;
using System.Runtime.InteropServices;

namespace MPL.Common.Win32
{
    /// <summary>
    /// A class that defines User32 functions.
    /// </summary>
    public static class User32Integration
    {
        #region Methods
        #region _Private_
        private static int SetListViewGroupStyle(IntPtr listViewHandle, int groupID, ListViewGroupStyle style)
        {
            IntPtr LParam = IntPtr.Zero;
            int ReturnValue = -1;

            try
            {
                LVGROUP ListViewGroupParam;
                IntPtr Result;

                // Build the LVGROUP and marhsal to a pointer
                ListViewGroupParam = new LVGROUP()
                {
                    cbSize = (uint)Marshal.SizeOf(typeof(LVGROUP)),
                    state = style,
                    mask = ListViewGroupFlags.STATE,
                    iGroupId = groupID
                };
                LParam = Marshal.AllocHGlobal((int)ListViewGroupParam.cbSize);
                Marshal.StructureToPtr(ListViewGroupParam, LParam, false);

                // Send the message to the ListView
                Result = NativeMethods.SendMessage(listViewHandle, ListViewMessage.LVM_SETGROUPINFO, new UIntPtr((uint)groupID), LParam);
                ReturnValue = Result.ToInt32();
            }
            catch (Exception)
            { }
            finally
            {
                // Clean up
                if (LParam != null && LParam != IntPtr.Zero)
                    Marshal.FreeHGlobal(LParam);
            }

            return ReturnValue;
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Gets the current cursor position on the screen.
        /// </summary>
        /// <param name="x">An int that will be set to the x-coordinate of the cursor.</param>
        /// <param name="y">An int that will be set to the y-coordinate of the cursor.</param>
        /// <returns>A bool indicating success.</returns>
        public static bool GetCursorPosition(out int x, out int y)
        {
            bool returnValue = false;

            if (NativeMethods.GetCursorPos(out POINT point))
            {
                x = point.x;
                y = point.y;
                returnValue = true;
            }
            else
            {
                x = 0;
                y = 0;
            }

            return returnValue;
        }

        /// <summary>
        /// Sets the current cursor position on the screen.
        /// </summary>
        /// <param name="x">An int indicating the x-coordinate of the cursor.</param>
        /// <param name="y">An int indicating the y-coordinate of the cursor.</param>
        /// <returns>A bool indicating success.</returns>
        public static bool SetCursorPosition(int x, int y)
        {
            return NativeMethods.SetCursorPos(x, y);
        }

        /// <summary>
        /// Sets the state of a list of ListView groups to a collapsible state.
        /// </summary>
        /// <param name="listViewHandle">An IntPtr containing the handle to the ListView.</param>
        /// <param name="groupID">An int containing the identifier of the group to set.</param>
        /// <returns>A bool indicating success.</returns>
        public static bool SetListViewGroupCollapsible(IntPtr listViewHandle, int groupID)
        {
            return (SetListViewGroupStyle(listViewHandle, groupID, ListViewGroupStyle.COLLAPSIBLE) == 0);
        }

        /// <summary>
        /// Sets the state of a list of ListView groups to a collapsible state.
        /// </summary>
        /// <param name="listViewHandle">An IntPtr containing the handle to the ListView.</param>
        /// <param name="groupIDs">An array of int containing the identifier of the groups to set.</param>
        /// <returns>A bool indicating success.</returns>
        public static bool SetListViewGroupsCollapsible(IntPtr listViewHandle, int[] groupIDs)
        {
            bool ReturnValue = false;

            if (listViewHandle != null && listViewHandle != IntPtr.Zero)
            {
                if (groupIDs != null)
                {
                    ReturnValue = true;
                    foreach (int Item in groupIDs)
                        if (!SetListViewGroupCollapsible(listViewHandle, Item))
                            ReturnValue = false;
                }
                else
                    throw new ArgumentException("The specified group list is NULL", "groupIDs");
            }
            else
                throw new ArgumentException("The specified ListView handle is invalid", "listViewHandle");

            return ReturnValue;
        }

        /// <summary>
        /// Sets the redraw state of the specified window.
        /// </summary>
        /// <param name="windowHandle">An IntPtr containing the handle to the window.</param>
        /// <param name="canRedraw">A bool indicating whether the window can be withdrawn.</param>
        /// <returns>A bool indicating success.</returns>
        public static bool SetWindowRedraw(IntPtr windowHandle, bool canRedraw)
        {
            bool ReturnValue = false;

            if (windowHandle != null && windowHandle != IntPtr.Zero)
            {
                IntPtr Result;

                Result = NativeMethods.SendMessage(windowHandle, WindowsMessage.WM_SETREDRAW, canRedraw ? 1 : 0, 0);
                if (Result.ToInt32() == 0)
                    ReturnValue = true;
            }
            else
                throw new ArgumentException("The specified window handle is invalid", "windowHandle");

            return ReturnValue;
        }

        #endregion
        #endregion
    }
}