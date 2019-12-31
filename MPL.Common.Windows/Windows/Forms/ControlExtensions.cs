using MPL.Common.Win32;
using System;
using System.Windows.Forms;

namespace MPL.Common.Windows.Forms
{
    /// <summary>
    /// A class that defines extension methods for controls.
    /// </summary>
    public static class ControlExtensions
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Prevents the control from drawing until the EndUpdate() method is called.
        /// </summary>
        /// <param name="control">A Control that will have drawing prevented.</param>
        public static void BeginUpdate(this Control control)
        {
            User32Integration.SetWindowRedraw(control.Handle, false);
        }

        /// <summary>
        /// Resumes drawing of the list view control after drawing is suspended by the BeginUpdate() method.
        /// </summary>
        /// <param name="control">A Control that will have drawing resumed.</param>
        public static void EndUpdate(this Control control)
        {
            User32Integration.SetWindowRedraw(control.Handle, true);
        }

        #endregion
        #endregion
    }
}