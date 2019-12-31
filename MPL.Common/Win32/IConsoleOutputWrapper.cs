using System;

namespace MPL.Common.Win32
{
    /// <summary>
    /// An interface that defines a wrapper around a console output.
    /// </summary>
    public interface IConsoleOutputWrapper
    {
        /// <summary>
        /// Writes the specified output to the console.
        /// </summary>
        /// <param name="value">An object containing the value to write.</param>
        void Write(object value);
        /// <summary>
        /// Writes the specified output to the console, formatted with the specified argument.
        /// </summary>
        /// <param name="format">A string containing the text to be formatted.</param>
        /// <param name="arg0">A string containing the argument for the text.</param>
        void Write(string format, object arg0);
        /// <summary>
        /// Writes the specified output to the console, formatted with the specified arguments.
        /// </summary>
        /// <param name="format">A string containing the text to be formatted.</param>
        /// <param name="arg0">A string containing the first argument for the text.</param>
        /// <param name="arg1">A string containing the second argument for the text.</param>
        void Write(string format, object arg0, object arg1);
        /// <summary>
        /// Writes the specified output to the console, formatted with the specified arguments.
        /// </summary>
        /// <param name="format">A string containing the text to be formatted.</param>
        /// <param name="arg0">A string containing the first argument for the text.</param>
        /// <param name="arg1">A string containing the second argument for the text.</param>
        /// <param name="arg2">A string containing the third argument for the text.</param>
        void Write(string format, object arg0, object arg1, object arg2);
        /// <summary>
        /// Writes the specified output to the console, formatted with the specified arguments.
        /// </summary>
        /// <param name="format">A string containing the text to be formatted.</param>
        /// <param name="arg0">A string containing the first argument for the text.</param>
        /// <param name="arg1">A string containing the second argument for the text.</param>
        /// <param name="arg2">A string containing the third argument for the text.</param>
        /// <param name="arg3">A string containing the fourth argument for the text.</param>
        void Write(string format, object arg0, object arg1, object arg2, object arg3);
        /// <summary>
        /// Writes the specified output to the console, formatted with the specified arguments.
        /// </summary>
        /// <param name="format">A string containing the text to be formatted.</param>
        /// <param name="args">An array of object containing the formatting arguments for the text.</param>
        void Write(string format, params object[] args);

        /// <summary>
        /// Writes an empty line to the console.
        /// </summary>
        void WriteLine();
        /// <summary>
        /// Writes the specified output to the console followed by a line break.
        /// </summary>
        /// <param name="value">An object containing the value to write.</param>
        void WriteLine(object value);
        /// <summary>
        /// Writes the specified output to the console, formatted with the specified argument followed by a line break.
        /// </summary>
        /// <param name="format">A string containing the text to be formatted.</param>
        /// <param name="arg0">A string containing the argument for the text.</param>
        void WriteLine(string format, object arg0);
        /// <summary>
        /// Writes the specified output to the console, formatted with the specified arguments followed by a line break.
        /// </summary>
        /// <param name="format">A string containing the text to be formatted.</param>
        /// <param name="arg0">A string containing the first argument for the text.</param>
        /// <param name="arg1">A string containing the second argument for the text.</param>
        void WriteLine(string format, object arg0, object arg1);
        /// <summary>
        /// Writes the specified output to the console, formatted with the specified arguments followed by a line break.
        /// </summary>
        /// <param name="format">A string containing the text to be formatted.</param>
        /// <param name="arg0">A string containing the first argument for the text.</param>
        /// <param name="arg1">A string containing the second argument for the text.</param>
        /// <param name="arg2">A string containing the third argument for the text.</param>
        void WriteLine(string format, object arg0, object arg1, object arg2);
        /// <summary>
        /// Writes the specified output to the console, formatted with the specified arguments followed by a line break.
        /// </summary>
        /// <param name="format">A string containing the text to be formatted.</param>
        /// <param name="arg0">A string containing the first argument for the text.</param>
        /// <param name="arg1">A string containing the second argument for the text.</param>
        /// <param name="arg2">A string containing the third argument for the text.</param>
        /// <param name="arg3">A string containing the fourth argument for the text.</param>
        void WriteLine(string format, object arg0, object arg1, object arg2, object arg3);
        /// <summary>
        /// Writes the specified output to the console, formatted with the specified arguments followed by a line break.
        /// </summary>
        /// <param name="format">A string containing the text to be formatted.</param>
        /// <param name="args">An array of object containing the formatting arguments for the text.</param>
        void WriteLine(string format, params object[] args);
    }
}