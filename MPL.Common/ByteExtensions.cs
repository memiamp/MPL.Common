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
        /// Compares two byte arrays and determines whether they and their contents are equal.
        /// </summary>
        /// <param name="array1">An array of byte that is the array to compare to.</param>
        /// <param name="array2">An array of byte that is the array to be compared.</param>
        /// <returns>A bool indicating the result</returns>
        /// <remarks>Passing in NULL arrays will cause an exception.</remarks>
        /// <exception cref="ArgumentNullException">Throw when either of the arrays are NULL.</exception>
        public static bool EqualTo(this byte[] array1, byte[] array2)
        {
            bool returnValue = true;

            if (array1 == null) throw new ArgumentNullException(nameof(array1));
            if (array2 == null) throw new ArgumentNullException(nameof(array2));

            if (array1.Length == array2.Length)
            {
                int len = array1.Length;

                for (int i = 0; i < len; i++)
                    if (array1[i] != array2[i])
                    {
                        returnValue = false;
                        break;
                    }
            }
            else
                returnValue = false;

            return returnValue;
        }

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