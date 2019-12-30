using System;
using System.Diagnostics;

namespace MPL.Common.Logging
{
    /// <summary>
    /// A class that defines a log writer.
    /// </summary>
    public static class LogWriter
    {
        #region Constructors
        static LogWriter()
        {
            DefaultPriority = LogFileEntryPriority.Information;
        }

        #endregion

        #region Methods
        #region _Private_
        private static void GetSourceDetails(out string componentName, out string operation)
        {
            StackTrace StackTrace;

            componentName = null;
            operation = null;

            StackTrace = new StackTrace();
            for (int i = 0; i < StackTrace.FrameCount; i++)
            {
                StackFrame Frame;

                Frame = StackTrace.GetFrame(i);
                if (Frame.GetMethod().DeclaringType != typeof(LogWriter))
                {
                    operation = Frame.GetMethod().Name;
                    componentName = Frame.GetMethod().DeclaringType.FullName;
                    break;
                }
            }
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Logs an exception.
        /// </summary>
        /// <param name="ex">An Exception that is the exception to log.</param>
        public static void LogException(Exception ex)
        {
            int Count = 0;
            LogFileEntry Entry;
            Exception InnerEx;
            string Message = string.Empty;

            InnerEx = ex;
            while (InnerEx != null)
            {
                string StackTrace = string.Empty;

                Message += string.Format("{0}: {1}|{2}||", Count, InnerEx.GetType().FullName, InnerEx.Message);
                Message += InnerEx.StackTrace.Replace("\r\n", "|");
                Message += "||";
                InnerEx = InnerEx.InnerException;
                Count++;
            }

            GetSourceDetails(out string Component, out string Operation);
            Entry = new LogFileEntry(LogFileEntryPriority.Error, Component, Operation, Message);
            LogFile.WriteEntry(Entry);
        }

        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="operation">A string containing the name of the operation being performed.</param>
        public static void LogMessage(string message)
        {
            LogMessage(DefaultPriority, message);
        }
        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="message">A string containing the message to be logged.</param>
        /// <param name="arg0">An object used to format the message string.</param>
        public static void LogMessage(string message, object arg0)
        {
            LogMessage(string.Format(message, arg0));
        }
        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="message">A string containing the message to be logged.</param>
        /// <param name="args">An object used to format the message string.</param>
        public static void LogMessage(string message, params object[] args)
        {
            LogMessage(string.Format(message, args));

        }
        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="priority">A LogFileEntryPriority indicating the priority of the message.</param>
        /// <param name="message">A string containing the message to be logged.</param>
        public static void LogMessage(LogFileEntryPriority priority, string message)
        {
            LogFileEntry Entry;

            GetSourceDetails(out string Component, out string Operation);
            Entry = new LogFileEntry(priority, Component, Operation, message);
            LogFile.WriteEntry(Entry);
        }
        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="priority">A LogFileEntryPriority indicating the priority of the message.</param>
        /// <param name="message">A string containing the message to be logged.</param>
        /// <param name="arg0">An object used to format the message string.</param>
        public static void LogMessage(LogFileEntryPriority priority, string message, object arg0)
        {
            LogMessage(priority, string.Format(message, arg0));
        }
        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="priority">A LogFileEntryPriority indicating the priority of the message.</param>
        /// <param name="message">A string containing the message to be logged.</param>
        /// <param name="args">An object used to format the message string.</param>
        public static void LogMessage(LogFileEntryPriority priority, string message, params object[] args)
        {
            LogMessage(priority, string.Format(message, args));
        }

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the default log priority.
        /// </summary>
        public static LogFileEntryPriority DefaultPriority { get; set; }

        #endregion
    }
}