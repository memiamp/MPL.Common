using System;

namespace MPL.Common
{
    /// <summary>
    /// A class that defines extensions to DateTime.
    /// </summary>
    public static class DateTimeExtensions
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Gets the specified datetime as an ISO 8601 string.
        /// </summary>
        /// <param name="dateTime">A DateTime that is the date to convert.</param>
        /// <returns>A string containing the date and time in ISO 8601 format.</returns>
        public static string ToIso8601(this DateTime dateTime)
        {
            return dateTime.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Gets the specified datetime as an ISO 8601 string with a Zulu timezone indicator.
        /// </summary>
        /// <param name="dateTime">A DateTime that is the date to convert.</param>
        /// <returns>A string containing the date and time in ISO 8601 format.</returns>
        public static string ToIso8601Z(this DateTime dateTime)
        {
            return dateTime.ToIso8601() + "Z";
        }

        #endregion
        #endregion
    }
}