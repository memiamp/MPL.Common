using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MPL.Common.Reflection
{
    /// <summary>
    /// A class that provides helper functions to find a type.
    /// </summary>
    public static class TypeFinder
    {
        #region Methods
        #region _Private_
        private static Assembly LoadAssembly(string assemblyPath, bool silentExceptions = true)
        {
            Assembly ReturnValue = null;

            try
            {
                if (File.Exists(assemblyPath))
                    ReturnValue = Assembly.LoadFrom(assemblyPath);
            }
            catch (Exception)
            {
                if (!silentExceptions)
                    throw;
            }

            return ReturnValue;
        }

        private static bool IsSystemAssembly(Assembly assembly)
        {
            return IsSystemAssembly(assembly.FullName);
        }
        private static bool IsSystemAssembly(string assemblyName)
        {
            return assemblyName.StartsWith("mscorlib") || assemblyName.StartsWith("System") || assemblyName.StartsWith("Microsoft");
        }

        #endregion
        #region _Public_
        /// <summary>
        /// Finds all types that are a subclass of the specified type, or if T is an interface, implements that interface.
        /// </summary>
        /// <typeparam name="T">The type to find subclasses of.</typeparam>
        /// <param name="excludeSystemAssemblies">A bool that indicates whether to exclude any Microsoft.Net system assemblies from the search.</param>
        /// <param name="includeAbstractTypes">A bool that indicates whether abstract types should be included.</param>
        /// <returns>An array of Type containing the types.</returns>
        public static Type[] FindAllTypesOf<T>(bool excludeSystemAssemblies = true, bool includeAbstractTypes = false)
        {
            bool isInterface;
            List<Type> returnValue;
            Type tType;

            tType = typeof(T);
            isInterface = tType.IsInterface;

            returnValue = new List<Type>();
            try
            {
                foreach (Assembly Item in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (!excludeSystemAssemblies ||
                        (excludeSystemAssemblies && !IsSystemAssembly(Item)))
                    {
                        foreach (Type type in Item.GetTypes())
                        {
                            if ((includeAbstractTypes || !type.IsAbstract) &&
                                ((isInterface && tType.IsAssignableFrom(type)) ||
                                type.IsSubclassOf(tType)))
                            {
                                returnValue.Add(type);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Unable to find types", ex);
            }

            return returnValue.ToArray();
        }


        /// <summary>
        /// Finds the first type matching the specified type name from the specified assembly.
        /// </summary>
        /// <param name="assembly">An Assembly that is the assembly to search.</param>
        /// <param name="typeName">A string containing the name of the type to find.</param>
        /// <param name="type">A Type that will be set to the found type.</param>
        /// <param name="onlyExportedTypes">A bool indicating whether to only look for exported (public) types.</param>
        /// <returns>A bool that indicates whether the type was found</returns>
        public static bool TryFindType(Assembly assembly, string typeName, out Type type, bool onlyExportedTypes = true)
        {
            bool ReturnValue = false;

            // Defaults
            type = null;

            if (assembly != null)
            {
                Type[] types;

                types = onlyExportedTypes ? assembly.GetExportedTypes() : assembly.GetTypes();
                foreach (Type SubItem in types)
                    if (SubItem.FullName == typeName)
                    {
                        type = SubItem;
                        ReturnValue = true;
                        break;
                    }
            }

            return ReturnValue;
        }
        /// <summary>
        /// Finds the first type matching the specified type name from the specified assembly.
        /// </summary>
        /// <param name="assemblyName">A string containing the assembly name that contains the type.</param>
        /// <param name="typeName">A string containing the name of the type to find.</param>
        /// <param name="type">A Type that will be set to the found type.</param>
        /// <param name="onlyExportedTypes">A bool indicating whether to only look for exported (public) types.</param>
        /// <returns>A bool that indicates whether the type was found</returns>
        public static bool TryFindType(string assemblyName, string typeName, out Type type, bool onlyExportedTypes = true)
        {
            Assembly TargetAssembly = null;

            // Defaults
            type = null;

            foreach (Assembly Item in System.AppDomain.CurrentDomain.GetAssemblies())
            {
                if (Item.FullName == assemblyName)
                {
                    TargetAssembly = Item;
                    break;
                }
            }

            if (TargetAssembly == null)
                TryLoadAssembly(assemblyName, out TargetAssembly);

            return TryFindType(TargetAssembly, typeName, out type, onlyExportedTypes);
        }
        /// <summary>
        /// Finds the first type matching the specified type name from all loaded assemblies.
        /// </summary>
        /// <param name="typeName">A string containing the name of the type to find.</param>
        /// <param name="type">A Type that will be set to the found type.</param>
        /// <param name="excludeSystemAssemblies">A bool that indicates whether to exclude any Microsoft.Net system assemblies from the search.</param>
        /// <param name="onlyExportedTypes">A bool indicating whether to only look for exported (public) types.</param>
        /// <returns>A bool that indicates whether the type was found</returns>
        public static bool TryFindType(string typeName, out Type type, bool excludeSystemAssemblies = true, bool onlyExportedTypes = true)
        {
            bool returnValue = false;
            Assembly[] targetAssemblies;

            // Defaults
            type = null;

            targetAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => !excludeSystemAssemblies || !IsSystemAssembly(x)).ToArray();
            if (targetAssemblies?.Length > 0)
            {
                foreach (var assembly in targetAssemblies)
                    if (TryFindType(assembly, typeName, out Type foundType, onlyExportedTypes))
                    {
                        type = foundType;
                        returnValue = true;
                        break;
                    }
            }

            return returnValue;
        }

        /// <summary>
        /// Tries to load the specified Assembly,
        /// </summary>
        /// <param name="assemblyName">A string containing the name of the Assembly to try and load.</param>
        /// <param name="assembly">An Assembly that will be set to the loaded Assembly.</param>
        /// <returns></returns>
        public static bool TryLoadAssembly(string assemblyName, out Assembly assembly)
        {
            bool ReturnValue = false;

            // Defaults
            assembly = null;

            // Append .dll if missing
            if (!assemblyName.ToLower().EndsWith(".dll"))
                assemblyName += ".dll";

            try
            {
                string[] Paths;

                Paths = new string[] { assemblyName,
                                       Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), assemblyName),
                                       Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName),
                                       Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", assemblyName)};

                for (int i = 0; i < Paths.Length; i++)
                {
                    assembly = LoadAssembly(Paths[i]);
                    if (assembly != null)
                    {
                        ReturnValue = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                ReturnValue = false;
            }

            return ReturnValue;
        }

        #endregion
        #endregion
    }
}