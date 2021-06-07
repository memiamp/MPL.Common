using System;

namespace MPL.Common.Reflection
{
    /// <summary>
    /// A class that helps to create instances of a type.
    /// </summary>
    /// <typeparam name="T">A T that is the type to be created.</typeparam>
    public static class InstanceCreator<T>
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Create an instance of the type with the specified name from the specified assmebly.
        /// </summary>
        /// <param name="assembly">A string containing the name of the assembly in which the type is found.</param>
        /// <param name="typeName">A string containing the name of the type to create.</param>
        /// <param name="parameters">An array of object that can contain parameters used to construct the target type.</param>
        /// <returns>A T that is the created instance.</returns>
        public static T CreateInstance(string assembly, string typeName, object[] parameters = null)
        {
            T returnValue;

            if (TypeFinder.TryFindType(assembly, typeName, out Type TheType))
                returnValue = CreateInstance(TheType, parameters);
            else
                throw new ArgumentException($"Cannot create type instance: Unable to locate the type '{typeName}'", nameof(typeName));

            return returnValue;
        }
        /// <summary>
        /// Create an instance of the type with the specified name.
        /// </summary>
        /// <param name="typeName">A string containing the name of the type to create.</param>
        /// <param name="parameters">An array of object that can contain parameters used to construct the target type.</param>
        /// <returns>A T that is the created instance.</returns>
        /// <remarks>The first found type that matches the specified type name will be created.</remarks>
        public static T CreateInstance(string typeName, object[] parameters = null)
        {
            T returnValue;

            if (TypeFinder.TryFindType(typeName, out Type TheType))
                returnValue = CreateInstance(TheType, parameters);
            else
                throw new ArgumentException($"Cannot create type instance: Unable to locate the type '{typeName}'", nameof(typeName));

            return returnValue;
        }

        /// <summary>
        /// Create an instance of the specified type.
        /// </summary>
        /// <param name="type">A Type that is the type to be created.</param>
        /// <param name="parameters">An array of object that can contain parameters used to construct the target type.</param>
        /// <returns>A T that is the created instance.</returns>
        public static T CreateInstance(Type type, object[] parameters = null)
        {
            T returnValue;

            try
            {
                object createdObject;

                if (parameters != null)
                    createdObject = Activator.CreateInstance(type, parameters);
                else
                    createdObject = Activator.CreateInstance(type);
                if (createdObject != null)
                {
                    if (createdObject is T castCreatedObejct)
                        returnValue = castCreatedObejct;
                    else
                        throw new InvalidOperationException("The created type instance is not of the expected Type");
                }
                else
                    throw new InvalidOperationException("The created type instance was null");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unable to create an instance of the requested type", ex);
            }

            return returnValue;
        }

        #endregion
        #endregion
    }
}