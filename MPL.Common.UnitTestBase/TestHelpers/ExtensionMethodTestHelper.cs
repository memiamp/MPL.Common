using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace MPL.Common.TestHelpers
{
    /// <summary>
    /// A class that defines functionality for helping with extension method testing.
    /// </summary>
    public static class ExtensionMethodTestHelper
    {
        #region Methods
        #region _Private_
        private static bool CheckTypeMatch(Type type1, Type type2)
        {
            bool returnValue = false;

            // Direct comparison
            if (type1 == type2)
                returnValue = true;
            else
            {
                // Look for generic types
                if (type1.IsGenericType || type2.IsGenericType)
                {
                    Type genericType1 = null;
                    Type genericType2 = null;

                    if (type1.IsGenericType)
                        genericType1 = type1.GetGenericTypeDefinition();
                    if (type2.IsGenericType)
                        genericType2 = type2.GetGenericTypeDefinition();

                    if (type1.IsGenericType && !type2.IsGenericType && genericType1 == type2)
                        returnValue = true;
                    else if (!type1.IsGenericType && type2.IsGenericType && type1 == genericType2)
                        returnValue = true;
                    else if (type1.IsGenericType && type2.IsGenericType && genericType1 == genericType2)
                        returnValue = true;
                }

                // Compare base types (in the case of T[]-based types)
                if (!returnValue &&
                    type1.BaseType != null && type1.BaseType != typeof(object) &&
                    type2.BaseType != null && type2.BaseType != typeof(object))
                {
                    Type baseType1 = type1;
                    Type baseType2 = type2;

                    if (type1.BaseType != null && type1.BaseType != typeof(object))
                        baseType1 = type1.BaseType;
                    if (type2.BaseType != null && type2.BaseType != typeof(object))
                        baseType2 = type2.BaseType;

                    if (baseType1 == type2 || baseType2 == type1 || baseType1 == baseType2)
                        returnValue = true;
                }
            }

            return returnValue;
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Gets an indication of whether the specified type contains the named extension method.
        /// </summary>
        /// <param name="sourceType">A Type that is the source (non-extended) type to check.</param>
        /// <param name="extensionType">A Type that is the extension type to check.</param>
        /// <param name="methodName">A string containing the name of the method to check.</param>
        /// <param name="parameters">An array of Type indicating the parameters of the extension method.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool ContainsNamedExtensionMethod(Type sourceType, Type extensionType, string methodName, Type[] parameters = null)
        {
            MethodInfo[] extensionMethods;
            bool returnValue = false;

            extensionMethods = GetExtensionMethods(sourceType, extensionType);
            if (extensionMethods != null)
            {
                foreach (MethodInfo extensionMethod in extensionMethods)
                    if (extensionMethod.Name == methodName)
                    {
                        ParameterInfo[] methodParameters;

                        methodParameters = extensionMethod.GetParameters();
                        if (methodParameters != null && methodParameters.Length > 0)
                        {
                            if (CheckTypeMatch(methodParameters[0].ParameterType, sourceType))
                            {
                                // Check whether to verify method signature
                                if (parameters != null && parameters.Length > 0)
                                {
                                    if (methodParameters.Length == parameters.Length + 1)
                                    {
                                        bool IsMatch = true;

                                        for (int i = 0; i < parameters.Length; i++)
                                            if (!methodParameters[i + 1].ParameterType.IsGenericParameter &&
                                                !CheckTypeMatch(methodParameters[i + 1].ParameterType, parameters[i]))
                                            {
                                                IsMatch = false;
                                                break;
                                            }

                                        if (IsMatch)
                                        {
                                            returnValue = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    // No additional parameters to verify
                                    returnValue = true;
                                    break;
                                }
                            }
                        }
                    }
            }

            return returnValue;
        }

        /// <summary>
        /// Gets extension methods for the specified type.
        /// </summary>
        /// <param name="sourceType">A Type that is the source (non-extended) type to check.</param>
        /// <param name="extensionType">A Type that is the extension type to check.</param>
        /// <returns>An array of MethodInfo containing the extension methods.</returns>
        public static MethodInfo[] GetExtensionMethods(Type sourceType, Type extensionType)
        {
            List<MethodInfo> returnValue;

            returnValue = new List<MethodInfo>();

            if (!extensionType.IsGenericType)
            {
                foreach (MethodInfo method in extensionType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                    if (method.IsDefined(typeof(ExtensionAttribute), false))
                    {
                        ParameterInfo[] parameters;

                        parameters = method.GetParameters();
                        if (parameters != null && parameters.Length > 0 && CheckTypeMatch(parameters[0].ParameterType, sourceType))
                            returnValue.Add(method);
                    }
            }

            return returnValue.ToArray();
        }

        #endregion
        #endregion
    }
}