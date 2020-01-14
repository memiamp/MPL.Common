using System;
using System.Reflection;

namespace MPL.Common
{
    /// <summary>
    /// A class that defines conversion functions.
    /// </summary>
    public static class ConvertTo
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Tries to get the value of the specified object as the specified type.
        /// </summary>
        /// <typeparam name="T">A T that is the type that the object will be cast as.</typeparam>
        /// <param name="Value">An object that is the object to be cast.</param>
        /// <param name="value">A T that is the cast object, or the default value of T.</param>
        /// <returns>A bool indicating success.</returns>
        public static bool GetObjectAsValue<T>(object Value, out T value)
        {
            bool ReturnValue = false;

            // Defaults
            value = default(T);

            if (Value != null)
            {
                if (Value is T)
                {
                    ReturnValue = true;
                    value = (T)Value;
                }
                else
                {
                    MethodInfo TryParseMethod;

                    TryParseMethod = typeof(T).GetMethod("TryParse", new Type[] { typeof(string), typeof(T).MakeByRefType() });
                    if (TryParseMethod != null)
                    {
                        object Result;
                        object[] TryParseParams;

                        TryParseParams = new object[] { Value.ToString(), null };
                        Result = TryParseMethod.Invoke(null, TryParseParams);
                        if (Result != null && Result is bool && (bool)Result)
                        {
                            value = (T)TryParseParams[1];
                            ReturnValue = true;
                        }
                    }
                }
            }

            return ReturnValue;
        }

        #endregion
        #endregion
    }
}