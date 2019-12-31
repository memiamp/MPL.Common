using System;

namespace MPL.Common.Win32
{
    /// <summary>
    /// A class that defines output to the Console.
    /// </summary>
    internal sealed class ConsoleOutput : IConsoleOutputWrapper
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        internal ConsoleOutput()
        { }

        #endregion

        #region Interfaces
        #region _IConsoleOutputWrapper_
        void IConsoleOutputWrapper.Write(object value)
        {
            Console.Write(value);
        }
        void IConsoleOutputWrapper.Write(string format, object arg0)
        {
            Console.Write(format, arg0);
        }
        void IConsoleOutputWrapper.Write(string format, object arg0, object arg1)
        {
            Console.Write(format, arg0, arg1);
        }
        void IConsoleOutputWrapper.Write(string format, object arg0, object arg1, object arg2)
        {
            Console.Write(format, arg0, arg1, arg2);
        }
        void IConsoleOutputWrapper.Write(string format, object arg0, object arg1, object arg2, object arg3)
        {
            Console.Write(format, arg0, arg1, arg2, arg3);
        }
        void IConsoleOutputWrapper.Write(string format, params object[] args)
        {
            Console.Write(format, args);
        }

        void IConsoleOutputWrapper.WriteLine()
        {
            Console.WriteLine();
        }
        void IConsoleOutputWrapper.WriteLine(object value)
        {
            Console.WriteLine(value);
        }
        void IConsoleOutputWrapper.WriteLine(string format, object arg0)
        {
            Console.WriteLine(format, arg0);
        }
        void IConsoleOutputWrapper.WriteLine(string format, object arg0, object arg1)
        {
            Console.WriteLine(format, arg0, arg1);
        }
        void IConsoleOutputWrapper.WriteLine(string format, object arg0, object arg1, object arg2)
        {
            Console.WriteLine(format, arg0, arg1, arg2);
        }
        void IConsoleOutputWrapper.WriteLine(string format, object arg0, object arg1, object arg2, object arg3)
        {
            Console.WriteLine(format, arg0, arg1, arg2, arg3);
        }
        void IConsoleOutputWrapper.WriteLine(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        #endregion
        #endregion
    }
}