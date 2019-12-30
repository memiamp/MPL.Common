using System;

namespace MPL.Common.Logging
{
    #region Declarations
    #region _Enumerations_
    /// <summary>
    /// An enumeration that defines the priority of a log file entry.
    /// </summary>
    public enum LogFileEntryPriority : int
    {
        /// <summary>
        /// The priority is undefined.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// The entry is of debug priority.
        /// </summary>
        Debug = 20,

        /// <summary>
        /// The entry is of error priority.
        /// </summary>
        Error = 5,

        /// <summary>
        /// The entry is of information priority.
        /// </summary>
        Information = 15,

        /// <summary>
        /// The entry is of warning priority.
        /// </summary>
        Warning = 10
    }

    #endregion
    #endregion
}