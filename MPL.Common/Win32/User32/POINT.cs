using System;
using System.Runtime.InteropServices;

namespace MPL.Common.Win32.User32
{
    /// <summary>
    /// A struct that defines the Win32 type POINT.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT
    {
        public int x;
        public int y;

        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="X">An int indicating the x parameter.</param>
        /// <param name="Y">An int indicating the y parameter.</param>
        internal POINT(int X, int Y)
        {
            x = X;
            y = Y;
        }
    }
}