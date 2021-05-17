using System;

namespace MPL.Common
{
    /// <summary>
    /// A class that defines extension methods for a string.
    /// </summary>
    public static class StringExtensions
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Counts all occurrences of the specified char in a string.
        /// </summary>
        /// <param name="haystack">A string that is the string to count occurrences in.</param>
        /// <param name="needle">A char that is the occurence to count.</param>
        /// <returns>An int indicating the number of occurrences.</returns>
        public static int CountAll(this string haystack, char needle)
        {
            int returnValue = 0;

            if (haystack != null)
            {
                int len = haystack.Length;

                for (int i = 0; i < len; i++)
                    if (haystack[i] == needle)
                        returnValue++;
            }

            return returnValue;
        }

        /// <summary>
        /// Counts all occurrences of the specified string in a string.
        /// </summary>
        /// <param name="haystack">A string that is the string to count occurrences in.</param>
        /// <param name="needle">A string that is the occurence to count.</param>
        /// <returns>An int indicating the number of occurrences.</returns>
        public static int CountAll(this string haystack, string needle)
        {
            int returnValue = 0;

            if (haystack != null)
                returnValue = (haystack.Length - haystack.Replace(needle, string.Empty).Length) / needle.Length;

            return returnValue;
        }

        #endregion
        #endregion
    }
}