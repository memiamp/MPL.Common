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
            bool returnValue = false;

            // Defaults
            value = default;

            if (Value != null)
            {
                if (Value is T)
                {
                    value = (T)Value;
                    returnValue = true;
                }
                else if (typeof(T).IsEnum)
                {
                    value = (T)Enum.Parse(typeof(T), Value.ToString());
                    returnValue = true;
                }
                else
                {
                    MethodInfo TryParseMethod;

                    TryParseMethod = typeof(T).GetMethod("TryParse", new Type[] { typeof(string), typeof(T).MakeByRefType() });
                    if (TryParseMethod != null)
                    {
                        object result;
                        object[] tryParseParams;

                        tryParseParams = new object[] { Value.ToString(), null };
                        result = TryParseMethod.Invoke(null, tryParseParams);
                        if (result != null && result is bool && (bool)result)
                        {
                            value = (T)tryParseParams[1];
                            returnValue = true;
                        }
                    }
                }
            }

            return returnValue;
        }

        #endregion
        #endregion
    }
}