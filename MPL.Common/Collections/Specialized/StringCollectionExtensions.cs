using System;
using System.Collections.Specialized;

namespace MPL.Common.Collections.Specialized
{
    /// <summary>
    /// A class that provides extensions to the Specialized StringCollection object.
    /// </summary>
    public static class StringCollectionExtensions
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Converts a StringCollection to an array of strings.
        /// </summary>
        /// <param name="stringCollection">A StringCollection that is the string collection to convert.</param>
        /// <returns>An Array of string converted from the string collection.</returns>
        public static string[] ToArray(this StringCollection stringCollection)
        {
            string[] ReturnValue;

            ReturnValue = new string[stringCollection.Count];
            stringCollection.CopyTo(ReturnValue, 0);

            return ReturnValue;
        }

        #endregion
        #endregion
    }
}