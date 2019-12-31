using System;
using System.Collections.Generic;

namespace MPL.Common.TestHelpers
{
    /// <summary>
    /// A class that provides testing for generic ILists.
    /// </summary>
    public static class ListTestHelper<T>
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Tests that the Add function returns the expected result.
        /// </summary>
        /// <param name="list">An IList of type T that is the list to test.</param>
        /// <param name="genericTypeGenerator">A Func of type T that provides a way of generic new instances of the generic type.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestAdd(IList<T> list, Func<T> genericTypeGenerator)
        {
            int baseCount;
            bool returnValue = true;

            baseCount = list.Count;
            for (int i = 0; i < 5; i++)
            {
                list.Add(genericTypeGenerator());
                if (list.Count != baseCount + i + 1)
                {
                    returnValue = false;
                    break;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Tests all functionality of the list.
        /// </summary>
        /// <param name="list">An IList of type T that is the list to test.</param>
        /// <param name="genericTypeGenerator">A Func of type T that provides a way of generic new instances of the generic type.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestAll(IList<T> list, Func<T> genericTypeGenerator)
        {
            bool returnValue;

            returnValue = TestAdd(list, genericTypeGenerator) &&
                          TestClear(list, genericTypeGenerator) &&
                          TestContains(list, genericTypeGenerator) &&
                          TestCopyTo(list, genericTypeGenerator) &&
                          TestCount(list, genericTypeGenerator) &&
                          TestEnumerator(list, genericTypeGenerator) &&
                          TestIndexer(list, genericTypeGenerator) &&
                          TestIndexOf(list, genericTypeGenerator) &&
                          TestInsert(list, genericTypeGenerator) &&
                          TestRemove(list, genericTypeGenerator) &&
                          TestRemoveAt(list, genericTypeGenerator);

            return returnValue;
        }

        /// <summary>
        /// Tests that the Clear function returns the expected result.
        /// </summary>
        /// <param name="list">An IList of type T that is the list to test.</param>
        /// <param name="genericTypeGenerator">A Func of type T that provides a way of generic new instances of the generic type.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestClear(IList<T> list, Func<T> genericTypeGenerator)
        {
            object newObject = new object();
            bool returnValue = false;

            if (list.Count == 0)
            {
                list.Add(genericTypeGenerator());
                list.Add(genericTypeGenerator());
                list.Add(genericTypeGenerator());
            }

            if (list.Count > 0)
            {
                list.Clear();
                if (list.Count == 0)
                    returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// Tests that the CopyTo function returns the expected result.
        /// </summary>
        /// <param name="list">An IList of type T that is the list to test.</param>
        /// <param name="genericTypeGenerator">A Func of type T that provides a way of generic new instances of the generic type.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestCopyTo(IList<T> list, Func<T> genericTypeGenerator)
        {
            T[] array;
            int baseCount;
            bool returnValue = true;

            list.Add(genericTypeGenerator());
            list.Add(genericTypeGenerator());
            list.Add(genericTypeGenerator());
            baseCount = list.Count;
            array = new T[baseCount];
            list.CopyTo(array, 0);

            for (int i = 0; i < list.Count; i++)
                if (!list[i].Equals(array[i]))
                {
                    returnValue = false;
                    break;
                }

            return returnValue;
        }

        /// <summary>
        /// Tests that the Contains function returns the expected result.
        /// </summary>
        /// <param name="list">An IList of type T that is the list to test.</param>
        /// <param name="genericTypeGenerator">A Func of type T that provides a way of generic new instances of the generic type.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestContains(IList<T> list, Func<T> genericTypeGenerator)
        {
            T newObject = genericTypeGenerator();
            bool returnValue = false;

            list.Add(newObject);
            if (list.Contains(newObject))
                returnValue = true;

            return returnValue;
        }

        /// <summary>
        /// Tests that the Count function returns the expected result.
        /// </summary>
        /// <param name="list">An IList of type T that is the list to test.</param>
        /// <param name="genericTypeGenerator">A Func of type T that provides a way of generic new instances of the generic type.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestCount(IList<T> list, Func<T> genericTypeGenerator)
        {
            int baseCount;
            bool returnValue = true;

            baseCount = list.Count;
            for (int i = 0; i < 5; i++)
            {
                list.Add(genericTypeGenerator());
                if (list.Count != baseCount + i + 1)
                {
                    returnValue = false;
                    break;
                }
            }

            if (!returnValue)
            {
                baseCount = list.Count;
                for (int i = 0; i < 3; i++)
                {
                    list.Remove(list[0]);
                    if (list.Count != baseCount - i - 1)
                    {
                        returnValue = false;
                        break;
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Tests that the Enumerator returns the expected result.
        /// </summary>
        /// <param name="list">An IList of type T that is the list to test.</param>
        /// <param name="genericTypeGenerator">A Func of type T that provides a way of generic new instances of the generic type.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestEnumerator(IList<T> list, Func<T> genericTypeGenerator)
        {
            int baseCount;
            int enumeratorCount = 0;
            bool returnValue = false;

            list.Add(genericTypeGenerator());
            list.Add(genericTypeGenerator());
            list.Add(genericTypeGenerator());
            baseCount = list.Count;
            foreach (object item in list)
                enumeratorCount++;
            if (baseCount == enumeratorCount)
                returnValue = true;

            return returnValue;
        }

        /// <summary>
        /// Tests that the indexer returns the expected result.
        /// </summary>
        /// <param name="list">An IList of type T that is the list to test.</param>
        /// <param name="genericTypeGenerator">A Func of type T that provides a way of generic new instances of the generic type.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestIndexer(IList<T> list, Func<T> genericTypeGenerator)
        {
            T newObject = genericTypeGenerator();
            bool returnValue = false;

            if (list.Count == 0)
            {
                list.Add(genericTypeGenerator());
                list.Add(genericTypeGenerator());
                list.Add(genericTypeGenerator());
            }

            list[0] = newObject;
            if (list[0].Equals(newObject))
                returnValue = true;

            return returnValue;
        }


        /// <summary>
        /// Tests that the IndexOf function returns the expected result.
        /// </summary>
        /// <param name="list">An IList of type T that is the list to test.</param>
        /// <param name="genericTypeGenerator">A Func of type T that provides a way of generic new instances of the generic type.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestIndexOf(IList<T> list, Func<T> genericTypeGenerator)
        {
            int index;
            bool returnValue = false;
            T newObject;

            newObject = genericTypeGenerator();
            list.Add(newObject);
            index = list.IndexOf(newObject);
            if (list[index].Equals(newObject))
                returnValue = true;

            return returnValue;
        }

        /// <summary>
        /// Tests that the Insert function returns the expected result.
        /// </summary>
        /// <param name="list">An IList of type T that is the list to test.</param>
        /// <param name="genericTypeGenerator">A Func of type T that provides a way of generic new instances of the generic type.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestInsert(IList<T> list, Func<T> genericTypeGenerator)
        {
            bool returnValue = false;
            T newObject;

            newObject = genericTypeGenerator();
            list.Add(genericTypeGenerator());
            list.Add(genericTypeGenerator());
            list.Add(genericTypeGenerator());
            list.Insert(1, newObject);
            if (list[1].Equals(newObject))
                returnValue = true;

            return returnValue;
        }

        /// <summary>
        /// Tests that the Remove function returns the expected result.
        /// </summary>
        /// <param name="list">An IList of type T that is the list to test.</param>
        /// <param name="genericTypeGenerator">A Func of type T that provides a way of generic new instances of the generic type.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestRemove(IList<T> list, Func<T> genericTypeGenerator)
        {
            int baseCount;
            T[] objects = new T[] { genericTypeGenerator(), genericTypeGenerator(), genericTypeGenerator() };
            bool returnValue = true;

            baseCount = list.Count;
            list.Add(objects[0]);
            list.Add(objects[1]);
            list.Add(objects[2]);
            if (list.Count == baseCount + 3)
            {
                list.Remove(objects[1]);
                if (list.Count == baseCount + 2)
                {
                    list.Remove(objects[2]);
                    if (list.Count == baseCount + 1)
                    {
                        list.Remove(objects[0]);
                        if (list.Count != baseCount)
                        {
                            returnValue = false;
                        }
                    }
                }
            }
            else
                returnValue = false;

            return returnValue;
        }

        /// <summary>
        /// Tests that the RemoveAt function returns the expected result.
        /// </summary>
        /// <param name="list">An IList of type T that is the list to test.</param>
        /// <param name="genericTypeGenerator">A Func of type T that provides a way of generic new instances of the generic type.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestRemoveAt(IList<T> list, Func<T> genericTypeGenerator)
        {
            int baseCount;
            bool returnValue = false;
            T newObject;

            newObject = genericTypeGenerator();
            list.Add(genericTypeGenerator());
            list.Add(genericTypeGenerator());
            list.Add(genericTypeGenerator());
            list.Insert(1, newObject);
            baseCount = list.Count;
            if (list[1].Equals(newObject))
            {
                list.RemoveAt(1);
                if (list.Count == baseCount - 1 && !list[1].Equals(newObject))
                    returnValue = true;
            }

            return returnValue;
        }

        #endregion
        #endregion
    }
}