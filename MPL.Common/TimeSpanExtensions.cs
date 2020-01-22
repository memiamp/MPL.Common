using System;

namespace MPL.Common
{
    /// <summary>
    /// A class that defines extensions to a TimeSpan.
    /// </summary>
    public static class TimeSpanExtensions
    {
        #region Methods
        #region _Private_
        private static bool ValueToOutput(int value, string name, string plural, bool outputZeroValues, out string output)
        {
            output = string.Empty;
            bool returnValue = false;

            if (value > 0)
            {
                if (value == 1)
                    output = $"1 {name}";
                else
                    output = $"{value} {plural}";
            }
            else
            {
                if (outputZeroValues)
                    output = $"0 {plural}";
            }

            if (!string.IsNullOrWhiteSpace(output))
                returnValue = true;

            return returnValue;
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Gets a textual respresentation of the specified TimeSpan without milliseconds or zero value outputs.
        /// </summary>
        /// <param name="timeSpan">A TimeSpan to get the textual representation of.</param>
        /// <returns>A string containing the textual representation.</returns>
        public static string ToTextualTimestamp(this TimeSpan timeSpan)
        {
            return ToTextualTimestamp(timeSpan, false, false);
        }
        /// <summary>
        /// Gets a textual respresentation of the specified TimeSpan.
        /// </summary>
        /// <param name="timeSpan">A TimeSpan to get the textual representation of.</param>
        /// <param name="outputZeroValues">A bool indicating whether to output zero values.</param>
        /// <param name="outputMilliseconds">A bool indicating whether to output millseconds.</param>
        /// <returns>A string containing the textual representation.</returns>
        public static string ToTextualTimestamp(this TimeSpan timeSpan, bool outputMilliseconds, bool outputZeroValues)
        {
            int counter = 0;
            string[] elements = new string[5];
            string returnValue = string.Empty;

            if (ValueToOutput(timeSpan.Days, "day", "days", outputZeroValues, out string output))
                elements[counter++] = output;
            if (ValueToOutput(timeSpan.Hours, "hour", "hours", outputZeroValues, out output))
                elements[counter++] = output;
            if (ValueToOutput(timeSpan.Minutes, "minute", "minutes", outputZeroValues, out output))
                elements[counter++] = output;
            if (ValueToOutput(timeSpan.Seconds, "second", "seconds", outputZeroValues, out output))
                elements[counter++] = output;
            if (outputMilliseconds && ValueToOutput(timeSpan.Milliseconds, "millisecond", "milliseconds", outputZeroValues, out output))
                elements[counter++] = output;

            for (int i = 0; i < counter; i++)
            {
                if (i != 0)
                {
                    if (i == counter - 1)
                    {
                        returnValue += $" and {elements[i]}";
                    }
                    else
                    {
                        returnValue += $", {elements[i]}";
                    }
                }
                else
                    returnValue = elements[i];
            }

            return returnValue;
        }

        #endregion
        #endregion
    }
}