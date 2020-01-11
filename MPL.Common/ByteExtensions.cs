using System;
using System.Linq;

namespace MPL.Common
{
    /// <summary>
    /// A class that defines extensions to byte.
    /// </summary>
    public static class ByteExtensions
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Converts an array of bytes to a hexadecimal string.
        /// </summary>
        /// <param name="array">An array of byte that is the byte array to convert.</param>
        /// <param name="delimiter">A string containing the delimiter text to use.</param>
        /// <param name="useLowerCase">A bool that indicates whether to use lower case. If not, upper case will be used.</param>
        /// <returns>A string that was converted from the byte array.</returns>
        public static string ToHexString(this byte[] array, string delimiter = " ", bool useLowerCase = true)
        {
            return string.Join(delimiter, array.Select(x => x.ToString(useLowerCase ? "x2" : "X2")));
        }

        #endregion
        #endregion
    }
}