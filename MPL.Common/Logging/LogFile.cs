using MPL.Common.Collections.Concurrent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace MPL.Common.Logging
{
    /// <summary>
    /// A class that defines a log file.
    /// </summary>
    public static class LogFile
    {
        #region Constructors
        static LogFile()
        {
            _Consoles = new ConcurrentList<IConsole>();
            LogFilePath = AppDomain.CurrentDomain.BaseDirectory;
            MaximumPriority = LogFileEntryPriority.Debug;
            _Messages = new SimpleConcurrentQueue<string>();
            _PreviousMessages = new List<string>();

            LogWriter.LogMessage("Created log file");

            // Start by default
            Start();
        }

        #endregion

        #region Declarations
        #region _Members_
        private static IList<IConsole> _Consoles;
        private static bool _IsRunning;
        private static StreamWriter _LogFile;
        private static SimpleConcurrentQueue<string> _Messages;
        private static List<string> _PreviousMessages;
        private static Thread _RunLogWriterThread;

        #endregion
        #endregion

        #region Methods
        #region _Internal_
        /// <summary>
        /// Writes the specified entry to the log file.
        /// </summary>
        /// <param name="entry">A LogFileEntry that is the entry to write.</param>
        internal static void WriteEntry(LogFileEntry entry)
        {
            if ((int)entry.Priority <= (int)MaximumPriority)
            {
                // Check for duplicates
                if (!_PreviousMessages.Contains(entry.Message))
                {
                    string Line;

                    _PreviousMessages.Add(entry.Message);
                    Line = ParseEntry(entry);
                    WriteLine(Line);
                }

                while (_PreviousMessages.Count > 5)
                    _PreviousMessages.RemoveAt(0);
            }
        }

        #endregion
        #region _Private_
        private static void CloseLogFile()
        {
            if (_LogFile != null)
            {
                try
                {
                    _LogFile.Close();
                }
                catch (Exception)
                { }
            }
        }

        private static void CreateLogFile()
        {
            CloseLogFile();

            // Create the filename
            FileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".txt";
            FileName = Path.Combine(LogFilePath, FileName);
            if (File.Exists(FileName))
                FileName = Path.Combine(LogFilePath, Path.GetFileNameWithoutExtension(FileName) + "_" + Guid.NewGuid().ToString() + ".txt");

            // Create the log file
            try
            {
                _LogFile = File.CreateText(FileName);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unable to create log file '{LogFilePath}'", ex);
            }
        }

        private static void OutputConsole(string entry)
        {
            foreach (IConsole Item in _Consoles)
            {
                Item.WriteEntry(entry);
            }
        }

        private static string ParseEntry(LogFileEntry entry)
        {
            string ReturnValue;
            string Timestamp;

            Timestamp = entry.Timestamp.ToString("yyyy-MM-dd HH-mm-ss.ffff");
            ReturnValue = $"{Timestamp},{entry.Priority},{entry.Component},{entry.Operation},{entry.Message}";

            return ReturnValue;
        }

        private static void RunLogWriter()
        {
            LogWriter.LogMessage("Started log file writer run thread");

            while (_IsRunning)
            {
                while (_Messages.Count > 0)
                {
                    string NextMessage;

                    NextMessage = _Messages.Dequeue();
                    if (NextMessage != null)
                    {
                        int Retries = 0;

                        if (_LogFile == null)
                            CreateLogFile();

                        OutputConsole(NextMessage);

                        while (Retries < 3)
                        {
                            try
                            {
                                _LogFile.WriteLine(NextMessage);
                                _LogFile.Flush();
                                Retries = 5;
                            }
                            catch (Exception ex)
                            {
                                LogWriter.LogException(ex);

                                // Try again?
                                CreateLogFile();
                                Retries++;
                            }
                        }
                    }
                }

                Thread.Sleep(1000);
            }

            LogWriter.LogMessage("Stopped log file writer run thread");
        }

        private static void WriteLine(string message)
        {
            _Messages.Enqueue(message);
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Adds a new output console to the log writer.
        /// </summary>
        /// <param name="console">An IConsole that is the console to add.</param>
        public static void AddConsole(IConsole console)
        {
            _Consoles.Add(console);
        }

        /// <summary>
        /// Starts the log file.
        /// </summary>
        public static void Start()
        {
            if (!_IsRunning)
            {
                LogWriter.LogMessage("Starting log file");

                _IsRunning = true;

                _RunLogWriterThread = new Thread(new ThreadStart(RunLogWriter)) { Name = "Log Writer Thread" };
                _RunLogWriterThread.Start();

                LogWriter.LogMessage("Log file started");
            }
        }

        /// <summary>
        /// Stops the log file.
        /// </summary>
        public static void Stop()
        {
            if (_IsRunning)
            {
                LogWriter.LogMessage("Stopping log file");

                _IsRunning = false;

                LogWriter.LogMessage("Log file stopped");
            }
        }

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets the full path and file name to the current log file.
        /// </summary>
        internal static string FileName { get; private set; }

        /// <summary>
        /// Gets or sets the path to create log files in.
        /// </summary>
        public static string LogFilePath { get; set; }

        /// <summary>
        /// Gets or sets the maximum logged priority.
        /// </summary>
        public static LogFileEntryPriority MaximumPriority { get; set; }

        #endregion
    }
}