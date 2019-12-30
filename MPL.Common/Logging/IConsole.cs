using System;

namespace MPL.Common.Logging
{
    /// <summary>
    /// A class that defines a console interface for the log writer.
    /// </summary>
    public interface IConsole
    {
        /// <summary>
        /// Writes an entry to the console.
        /// </summary>
        /// <param name="entry">A string containing the content of the entry.</param>
        void WriteEntry(string entry);
    }
}