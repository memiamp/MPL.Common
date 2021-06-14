using System;
using System.Linq;
using System.Reflection;

namespace MPL.Common.Reflection
{
    /// <summary>
    /// A class that helps to invoke methods on types.
    /// </summary>
    public static class MethodInvoker
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Invokes the specified method of the specified interface.
        /// </summary>
        /// <param name="obj">An object that is the instance to invoke the method on.</param>
        /// <param name="targetInterfaceType">A Type indicating the target type for method invocation.</param>
        /// <param name="methodName">A string containing the name of the method to invoke.</param>
        /// <param name="methodParams">An array of object containing the parameters for the method.</param>
        public static void InvokeInterfaceMethod(object obj, Type targetInterfaceType, string methodName, object[] methodParams = null)
        {
            if (obj != null)
            {
                Type interfaceType;

                interfaceType = obj.GetType().GetInterfaces().Where(x => x.Name == targetInterfaceType.Name).FirstOrDefault();
                if (interfaceType != null)
                {
                    MethodInfo targetMethod;

                    targetMethod = interfaceType.GetMethod(methodName);
                    if (targetMethod != null)
                    {
                        targetMethod.Invoke(obj, methodParams);
                    }
                    else
                        throw new InvalidOperationException("Unable to create instance of target method type");
                }
                else
                    throw new InvalidOperationException("Unable to locate required type");
            }
            else
                throw new ArgumentNullException(nameof(obj));
        }

        /// <summary>
        /// Invokes the specified method of the specified interface.
        /// </summary>
        /// <typeparam name="T">The Type of the value to be returned from the method.  Do not use for void return types.</typeparam>
        /// <param name="obj">An object that is the instance to invoke the method on.</param>
        /// <param name="targetInterfaceType">A Type indicating the target type for method invocation.</param>
        /// <param name="methodName">A string containing the name of the method to invoke.</param>
        /// <param name="methodParams">An array of object containing the parameters for the method.</param>
        /// <returns>A T that was returned by the invoked method.</returns>
        public static T InvokeInterfaceMethod<T>(object obj, Type targetInterfaceType, string methodName, object[] methodParams = null)
        {
            T returnValue = default;

            if (obj != null)
            {
                Type interfaceType;

                interfaceType = obj.GetType().GetInterfaces().Where(x => x.Name == targetInterfaceType.Name).FirstOrDefault();
                if (interfaceType != null)
                {
                    MethodInfo targetMethod;

                    targetMethod = interfaceType.GetMethod(methodName);
                    if (targetMethod != null)
                    {
                        object resultObj;

                        resultObj = targetMethod.Invoke(obj, methodParams);
                        if (resultObj != null && resultObj is T result)
                            returnValue = result;
                        else
                            throw new InvalidOperationException("Result returned from method is NULL or is not required type.");
                    }
                    else
                        throw new InvalidOperationException("Unable to create instance of target method type");
                }
                else
                    throw new InvalidOperationException("Unable to locate required type");
            }
            else
                throw new ArgumentNullException(nameof(obj));

            return returnValue;
        }

        #endregion
        #endregion
    }
}