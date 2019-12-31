using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Security.Cryptography;

namespace MPL.Common.TestHelpers
{
    /// <summary>
    /// A class that defines helper functions for testing.
    /// </summary>
    public static class TestDataTestHelper
    {
        #region Declarations
        #region _Members_
        private static RNGCryptoServiceProvider _RNG;

        #endregion
        #endregion

        #region Methods
        #region _Public_
        /// <summary>
        /// Tests whether the specified values match the specified fields.
        /// </summary>
        /// <param name="values">An array of object containing the values to test.</param>
        /// <param name="fields">An array of object containing the fields to test.</param>
        public static void AreEqual(object[] values, params object[] fields)
        {
            if (values == null || values.Length < 1)
                throw new ArgumentException("The specified values array is null or empty", "values");
            if (fields == null || fields.Length < 1)
                throw new ArgumentException("The specified values array is null or empty", "fields");
            if (values.Length != fields.Length)
                throw new ArgumentException("The specified fields array is a different length than the values array", "fields");

            for (int i = 0; i < values.Length; i++)
                Assert.AreEqual(values[i], fields[i]);
        }

        /// <summary>
        /// Fills the specified byte array with randomly generated data.
        /// </summary>
        /// <param name="buffer">An array of byte that will be filled with the data.</param>
        public static void GetBytes(byte[] buffer)
        {
            if (buffer != null && buffer.Length > 0)
                RNG.GetBytes(buffer);
            else
                throw new ArgumentException("The specified buffer is null or zero-length", "buffer");
        }
        /// <summary>
        /// Gets a byte array containing randomly generated data.
        /// </summary>
        /// <param name="bufferSize">An int indicating the size of the byte array to be returned.</param>
        /// <returns>An array of byte containing the results.</returns>
        public static byte[] GetBytes(int bufferSize)
        {
            byte[] ReturnValue;

            ReturnValue = new byte[bufferSize];
            GetBytes(ReturnValue);

            return ReturnValue;
        }
        /// <summary>
        /// Gets a randomly-size byte array containing randomly generated data.
        /// </summary>
        /// <param name="minimumBufferSize">An int indicating the minimum size of the returned byte buffer.</param>
        /// <param name="maximumBufferSize">An int inficating the maximum size of the returned byte buffer.</param>
        /// <returns>An array of byte containing the results.</returns>
        public static byte[] GetBytes(int minimumBufferSize = 4, int maximumBufferSize = 100)
        {
            byte[] ReturnValue;
            int TargetLength;

            if (minimumBufferSize > 0)
            {
                TargetLength = GetInt(minimumBufferSize, maximumBufferSize + 1);
                ReturnValue = GetBytes(TargetLength);
            }
            else
                throw new ArgumentException(string.Format("The specified buffer size '{0}' is invalid", minimumBufferSize), "minimumBufferSize");

            return ReturnValue;
        }

        /// <summary>
        /// Gets a randomly generated DateTime value.
        /// </summary>
        /// <returns>A DateTime that was randomly generated.</returns>
        public static DateTime GetDateTime()
        {
            DateTime ReturnValue;
            long Ticks;

            Ticks = GetInt(0, 694223999) + 62987673600;
            Ticks *= 10000000;
            ReturnValue = new DateTime(Ticks);

            return ReturnValue;
        }

        /// <summary>
        /// Gets a randomly generated float.
        /// </summary>
        /// <returns>A float that was generated randomly.</returns>
        public static float GetFloat()
        {
            return BitConverter.ToSingle(GetBytes(8), 0);
        }

        /// <summary>
        /// Gets a randomly generated integer value.
        /// </summary>
        /// <param name="minimumValue">An int indicating the minimum value of the integer.</param>
        /// <param name="maximumValue">An int indicating the maximum value of the integer.</param>
        /// <returns>An int that was generated randomly within the specified range.</returns>
        public static int GetInt(int minimumValue = 0, int maximumValue = int.MaxValue)
        {
            long Difference;
            int ReturnValue;
            long UpperBound;
            uint Value;

            Difference = (long)maximumValue - minimumValue;
            UpperBound = uint.MaxValue / Difference * Difference;

            do
            {
                Value = GetUint();
            } while (Value >= UpperBound);

            ReturnValue = (int)(minimumValue + (Value % Difference));

            return ReturnValue;
        }

        /// <summary>
        /// Gets a randomly generated parameter for the specified type.
        /// </summary>
        /// <param name="type">A Type that is the type to get the parameter for.</param>
        /// <returns>An object that was randomly generated.</returns>
        public static object GetParameter(Type type)
        {
            object ReturnValue = null;

            switch (type.Name)
            {
                case "Byte":
                    ReturnValue = GetBytes(1)[0];
                    break;

                case "Byte[]":
                    ReturnValue = GetBytes();
                    break;

                case "DateTime":
                    ReturnValue = GetDateTime();
                    break;

                case "Int32":
                    ReturnValue = GetInt();
                    break;

                case "String":
                    ReturnValue = GetString();
                    break;

                default:
                    if (type.IsValueType)
                        ReturnValue = Activator.CreateInstance(type);
                    break;
            }

            return ReturnValue;
        }

        /// <summary>
        /// Gets randomly generated parameters for the specified method.
        /// </summary>
        /// <param name="method">A MethodBase that is the method to get values for.</param>
        /// <returns>An array of object containing the generated values.</returns>
        public static object[] GetParameters(MethodBase method)
        {
            ParameterInfo[] Parameters;
            object[] ReturnValue;
            Type[] TheTypes;

            Parameters = method.GetParameters();
            TheTypes = new Type[Parameters.Length];
            for (int i = 0; i < TheTypes.Length; i++)
                TheTypes[i] = Parameters[i].ParameterType;
            ReturnValue = GetParameters(TheTypes);

            return ReturnValue;
        }
        /// <summary>
        /// Gets randomly generated constructor parameter values for the specified type.
        /// </summary>
        /// <param name="type">A Type that is the type to get values for.</param>
        /// <returns>An array of object containing the generated values.</returns>
        public static object[] GetParameters(Type type)
        {
            object[] ReturnValue;

            if (type.GetConstructors().Length > 1)
                throw new ArgumentException("The specified type has more than one public constructor", "type");

            ReturnValue = GetParameters(type.GetConstructors()[0]);

            return ReturnValue;
        }
        /// <summary>
        /// Gets randomly generated parameter values for the specified method on the specified type.
        /// </summary>
        /// <param name="type">A Type that is the type to get values for.</param>
        /// <param name="methodName">A string containing the name of the method to generate parameters for.</param>
        /// <returns>An array of object containing the generated values.</returns>
        public static object[] GetParameters(Type type, string methodName)
        {
            MethodInfo Method;
            object[] ReturnValue;

            Method = type.GetMethod(methodName);
            if (Method != null)
                ReturnValue = GetParameters(Method);
            else
                throw new ArgumentException(string.Format("The specified method '{0}' does not exist in type '{1}'", methodName, type), "methodName");

            return ReturnValue;
        }
        /// <summary>
        /// Gets randomly generated values for the specified types.
        /// </summary>
        /// <param name="types">An array of Type containing the types to get values for.</param>
        /// <returns>An array of object containing the generated values.</returns>
        public static object[] GetParameters(Type[] types)
        {
            object[] ReturnValue;

            if (types == null || types.Length == 0)
                throw new ArgumentException("The specified type array is null or empty", "types");

            ReturnValue = new object[types.Length];
            for (int i = 0; i < ReturnValue.Length; i++)
                ReturnValue[i] = GetParameter(types[i]);

            return ReturnValue;
        }

        /// <summary>
        /// Gets a randomly generated string.
        /// </summary>
        /// <param name="minimumLength">An int indicating the inclusive minimum length of the string.</param>
        /// <param name="maximumLength">An int indicating the inclusive maximum length of the string.</param>
        /// <param name="charSet">A string containing the charset to use for the string.</param>
        /// <returns>A string that was randomly generated.</returns>
        public static string GetString(int minimumLength = 1, int maximumLength = 20, string charSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")
        {
            string ReturnValue = string.Empty;
            int TargetLength;

            if (minimumLength < 1)
                throw new ArgumentException(string.Format("The specified string length '{0}' is invalid", minimumLength), "minimumLength");

            TargetLength = GetInt(minimumLength, maximumLength + 1);
            for (int i = 0; i < TargetLength; i++)
                ReturnValue += charSet[GetInt(0, charSet.Length)];

            return ReturnValue;
        }

        /// <summary>
        /// Gets randomly generated strings.
        /// </summary>
        /// <param name="arraySize">An int indicating the size of the returned array.</param>
        /// <param name="minimumLength">An int indicating the inclusive minimum length of the string.</param>
        /// <param name="maximumLength">An int indicating the inclusive maximum length of the string.</param>
        /// <param name="charSet">A string containing the charset to use for the string.</param>
        /// <returns>An array of string that was randomly generated.</returns>
        public static string[] GetStrings(int arraySize, int minimumLength = 1, int maximumLength = 20, string charSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")
        {
            string[] ReturnValue;

            ReturnValue = new string[arraySize];
            GetStrings(ReturnValue, minimumLength, maximumLength, charSet);

            return ReturnValue;
        }
        /// <summary>
        /// Gets randomly generated strings.
        /// </summary>
        /// <param name="minimumArraySize">An int indicating the minimum number of strings in the array.</param>
        /// <param name="maximumArraySize">An int indicating the maximum number of strings in the array.</param>
        /// <param name="minimumLength">An int indicating the inclusive minimum length of the string.</param>
        /// <param name="maximumLength">An int indicating the inclusive maximum length of the string.</param>
        /// <param name="charSet">A string containing the charset to use for the string.</param>
        /// <returns>An array of string that was randomly generated.</returns>
        public static string[] GetStrings(int minimumArraySize = 4, int maximumArraySize = 20, int minimumLength = 1, int maximumLength = 20, string charSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")
        {
            string[] ReturnValue;

            ReturnValue = new string[GetInt(minimumArraySize, maximumArraySize)];
            GetStrings(ReturnValue, minimumLength, maximumLength, charSet);

            return ReturnValue;
        }
        /// <summary>
        /// Gets randomly generated strings.
        /// </summary>
        /// <param name="array">An array of string that will contain the generate strings.</param>
        /// <param name="minimumLength">An int indicating the inclusive minimum length of the string.</param>
        /// <param name="maximumLength">An int indicating the inclusive maximum length of the string.</param>
        /// <param name="charSet">A string containing the charset to use for the string.</param>
        /// <returns>A string that was randomly generated.</returns>
        public static void GetStrings(string[] array, int minimumLength = 1, int maximumLength = 20, string charSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = GetString(minimumLength, maximumLength, charSet);
        }

        /// <summary>
        /// Gets a randomly generated uint.
        /// </summary>
        /// <returns>An uint that was randomly generated.</returns>
        public static uint GetUint()
        {
            byte[] Data;
            uint ReturnValue;

            Data = GetBytes(sizeof(uint));
            ReturnValue = BitConverter.ToUInt32(Data, 0);

            return ReturnValue;
        }

        /// <summary>
        /// Invokes the specified method of the specified type with randomly generated parameter values.
        /// </summary>
        /// <param name="type">A Type that is the type to invoke the method on.</param>
        /// <param name="methodName">A string containing the name of the method to invoke.</param>
        /// <param name="instance">An object containing the instance against which to invoke the method.</param>
        /// <returns>An object containing any return result from the method invocation.</returns>
        public static object Invoke(Type type, string methodName, object instance)
        {
            MethodInfo Method;
            object ReturnValue;

            Method = type.GetMethod(methodName);
            if (Method != null)
            {
                object[] Parameters;

                Parameters = GetParameters(Method);
                ReturnValue = Method.Invoke(instance, Parameters);
            }
            else
                throw new ArgumentException(string.Format("The specified method '{0}' does not exist in type '{1}'", methodName, type), "methodName");

            return ReturnValue;
        }

        /// <summary>
        /// Mocks the specified collection, optionally prepopulating it with the specified number of entries.
        /// </summary>
        /// <typeparam name="T">A T that is the collection to mock.</typeparam>
        /// <param name="prepopulateSize">An int that indicates the number of entries to prepopulate.</param>
        /// <returns>A T that was the created type instance.</returns>
        public static T MockCollection<T>(int prepopulateSize = 0)
        {
            Type MyType;
            T ReturnValue;

            MyType = typeof(T);
            if (MyType.GetInterface("ICollection") == null &&
                MyType.GetInterface("ICollection`1") == null)
                throw new ArgumentException("The specified type does not implement ICollection", "T");

            ReturnValue = Activator.CreateInstance<T>();

            // Prepopulate values?
            if (prepopulateSize > 0)
            {
                Type GenericType = null;

                if (MyType.IsGenericType)
                    GenericType = MyType;
                else if (MyType.BaseType != null && MyType.BaseType.IsGenericType)
                    GenericType = MyType.BaseType;

                // Check for generic-based collections
                if (GenericType != null)
                {
                    if (GenericType.GenericTypeArguments != null && GenericType.GenericTypeArguments.Length == 1)
                    {
                        MethodInfo MockMethod;

                        MockMethod = typeof(TestDataTestHelper).GetMethod("MockType", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                        if (MockMethod != null)
                        {
                            MockMethod = MockMethod.MakeGenericMethod(GenericType.GenericTypeArguments[0]);
                            if (MockMethod != null)
                            {
                                MethodInfo AddMethod;

                                AddMethod = MyType.GetMethod("Add");
                                for (int i = 0; i < prepopulateSize; i++)
                                {
                                    object NewItem;

                                    NewItem = MockMethod.Invoke(null, new object[] { null });
                                    AddMethod.Invoke(ReturnValue, new object[] { NewItem });
                                }
                            }

                        }
                    }
                    else
                        throw new ArgumentException("Unable to mock generic collections with more than one generic type", "T");
                }
                else
                {
                    // Non generic-collection
                    throw new ArgumentException("Currently unable to mock non-generic collections", "T");
                }
            }

            return ReturnValue;
        }

        /// <summary>
        /// Mocks the specified type with random data.
        /// </summary>
        /// <typeparam name="T">A T that is the type to be mocked.</typeparam>
        /// <param name="parameters">An array of object that will contain the random data used to mock the object.</param>
        /// <returns>A T that is the created type instance.</returns>
        public static T MockType<T>(out object[] parameters)
        {
            T ReturnValue = default(T);

            parameters = GetParameters(typeof(T));
            ReturnValue = (T)Activator.CreateInstance(typeof(T), parameters);

            return ReturnValue;
        }


        /// <summary>
        /// Tests the specified collection, prepopulating it with 3 entries.
        /// </summary>
        /// <typeparam name="T">A T that is the collection to test.</typeparam>
        public static void TestCollection<T>()
        {
            TestCollection<T>(3);
        }
        /// <summary>
        /// Tests the specified collection, optionally prepopulating it with the specified number of entries.
        /// </summary>
        /// <typeparam name="T">A T that is the collection to test.</typeparam>
        /// <param name="prepopulateSize">An int that indicates the number of entries to prepopulate.</param>
        public static void TestCollection<T>(int prepopulateSize = 1)
        {
            if (prepopulateSize > 0)
            {
                T Collection;

                Collection = MockCollection<T>(prepopulateSize);
                TestCollection(Collection);
            }
            else
                throw new ArgumentException("The specified prepopulation size must be at least 1", "prepopulateSize");
        }
        /// <summary>
        /// Tests that common aspects of the specified collection are functional.
        /// </summary>
        /// <param name="collection">An object that is the collection to be tested.</param>
        public static void TestCollection(object collection)
        {
            if (collection != null)
            {
                Type MyType;

                MyType = collection.GetType();
                if (MyType.GetInterface("ICollection") != null)
                {
                    Type[] GenericTypes;

                    GenericTypes = MyType.BaseType.GetGenericArguments();
                    if (GenericTypes != null && GenericTypes.Length == 1)
                    {
                        MethodInfo AddMethod;
                        PropertyInfo CountProperty;
                        PropertyInfo IndexerProperty;
                        MethodInfo RemoveMethod;

                        AddMethod = MyType.GetMethod("Add");
                        CountProperty = MyType.GetProperty("Count");
                        IndexerProperty = MyType.GetProperty("Item", GenericTypes[0], new Type[] { typeof(int) });
                        RemoveMethod = MyType.GetMethod("Remove");

                        if (AddMethod != null && RemoveMethod != null && CountProperty != null && IndexerProperty != null)
                        {
                            int CurrentCount;

                            CurrentCount = (int)CountProperty.GetValue(collection);
                            if (CurrentCount > 0)
                            {
                                object TestObject;

                                TestObject = IndexerProperty.GetValue(collection, new object[] { 0 });
                                if (TestObject != null)
                                {
                                    Assert.AreEqual(CurrentCount, CountProperty.GetValue(collection));
                                    RemoveMethod.Invoke(collection, new object[] { TestObject });
                                    Assert.AreEqual(CurrentCount - 1, CountProperty.GetValue(collection));
                                    AddMethod.Invoke(collection, new object[] { TestObject });
                                    Assert.AreEqual(CurrentCount, CountProperty.GetValue(collection));
                                }
                                else
                                    Assert.Fail("Indexer returned a null result");
                            }
                            else
                                throw new ArgumentException("The specified collection is empty and cannot be tested", "collection");
                        }
                        else
                            throw new ArgumentException("The specified type does not support required operations", "collection");
                    }
                    else
                        throw new ArgumentException("The specified type does not implement a generic collection", "collection");
                }
                else
                    throw new ArgumentException("The specified type does not implement ICollection", "collection");
            }
            else
                throw new ArgumentException("The specified collection is null", "collection");
        }

        #endregion
        #endregion

        #region Properties
        private static RNGCryptoServiceProvider RNG
        {
            get
            {
                if (_RNG == null)
                    _RNG = new RNGCryptoServiceProvider();

                return _RNG;
            }
        }
        #endregion
    }
}