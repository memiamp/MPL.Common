using System;

namespace MPL.Common.Logging
{
    /// <summary>
    /// A class that defines an entry in the log file.
    /// </summary>
    internal class LogFileEntry
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="component">A string containing the name of the component that created the entry.</param>
        /// <param name="operation">A string containing the name of the operation being executed when the entry was created.</param>
        /// <param name="message">A string containing the message of the entry.</param>
        internal LogFileEntry(string component, string operation, string message)
            : this(LogFileEntryPriority.Information, component, operation, message)
        { }

        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="priority">A LogFileEntryPriority indicating the priority of the entry.</param>
        /// <param name="component">A string containing the name of the component that created the entry.</param>
        /// <param name="operation">A string containing the name of the operation being executed when the entry was created.</param>
        /// <param name="message">A string containing the message of the entry.</param>
        internal LogFileEntry(LogFileEntryPriority priority, string component, string operation, string message)
            : this(DateTime.Now, priority, component, operation, message)
        { }

        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="timestamp">A DateTime specifying the timestamp of the entry.</param>
        /// <param name="priority">A LogFileEntryPriority indicating the priority of the entry.</param>
        /// <param name="component">A string containing the name of the component that created the entry.</param>
        /// <param name="operation">A string containing the name of the operation being executed when the entry was created.</param>
        /// <param name="message">A string containing the message of the entry.</param>
        internal LogFileEntry(DateTime timestamp, LogFileEntryPriority priority, string component, string operation, string message)
        {
            Timestamp = timestamp;
            Priority = priority;
            Component = component;
            Operation = operation;
            Message = message;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets the name of the component that created the entry.
        /// </summary>
        internal string Component { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        internal string Message { get; }

        /// <summary>
        /// Gets the operation being executed when the entry was created.
        /// </summary>
        internal string Operation { get; }

        /// <summary>
        /// Gets the priority of the entry.
        /// </summary>
        internal LogFileEntryPriority Priority { get; }

        /// <summary>
        /// Gets the date and time of the entry.
        /// </summary>
        internal DateTime Timestamp { get; }

        #endregion
    }
}