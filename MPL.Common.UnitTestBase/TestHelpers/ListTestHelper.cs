using System;
using System.Collections;

namespace MPL.Common.TestHelpers
{
    /// <summary>
    /// A class that provides testing for ILists.
    /// </summary>
    public static class ListTestHelper
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Tests that the Add function returns the expected result.
        /// </summary>
        /// <param name="list">An IList that is the list to test.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestAdd(IList list)
        {
            int baseCount;
            bool returnValue = true;

            baseCount = list.Count;
            for (int i = 0; i < 5; i++)
            {
                list.Add(new object());
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
        /// <param name="list">An IList that is the list to test.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestAll(IList list)
        {
            bool returnValue;

            returnValue = TestAdd(list) &&
                          TestClear(list) &&
                          TestContains(list) &&
                          TestCopyTo(list) &&
                          TestCount(list) &&
                          TestEnumerator(list) &&
                          TestIndexer(list) &&
                          TestIndexOf(list) &&
                          TestInsert(list) &&
                          TestRemove(list) &&
                          TestRemoveAt(list);

            return returnValue;
        }

        /// <summary>
        /// Tests that the Clear function returns the expected result.
        /// </summary>
        /// <param name="list">An IList that is the list to test.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestClear(IList list)
        {
            object newObject = new object();
            bool returnValue = false;

            if (list.Count == 0)
            {
                list.Add(new object());
                list.Add(new object());
                list.Add(new object());
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
        /// <param name="list">An IList that is the list to test.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestCopyTo(IList list)
        {
            object[] array;
            int baseCount;
            bool returnValue = true;

            list.Add(new object());
            list.Add(new object());
            list.Add(new object());
            baseCount = list.Count;
            array = new object[baseCount];
            list.CopyTo(array, 0);

            for (int i = 0; i < list.Count; i++)
                if (list[i] != array[i])
                {
                    returnValue = false;
                    break;
                }

            return returnValue;
        }

        /// <summary>
        /// Tests that the Contains function returns the expected result.
        /// </summary>
        /// <param name="list">An IList that is the list to test.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestContains(IList list)
        {
            object newObject = new object();
            bool returnValue = false;

            list.Add(newObject);
            if (list.Contains(newObject))
                returnValue = true;

            return returnValue;
        }

        /// <summary>
        /// Tests that the Count function returns the expected result.
        /// </summary>
        /// <param name="list">An IList that is the list to test.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestCount(IList list)
        {
            int baseCount;
            bool returnValue = true;

            baseCount = list.Count;
            for (int i = 0; i < 5; i++)
            {
                list.Add(new object());
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
        /// <param name="list">An IList that is the list to test.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestEnumerator(IList list)
        {
            int baseCount;
            int enumeratorCount = 0;
            bool returnValue = false;

            list.Add(new object());
            list.Add(new object());
            list.Add(new object());
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
        /// <param name="list">An IList that is the list to test.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestIndexer(IList list)
        {
            object newObject = new object();
            bool returnValue = false;

            if (list.Count == 0)
            {
                list.Add(new object());
                list.Add(new object());
                list.Add(new object());
            }

            list[0] = newObject;
            if (list[0] == newObject)
                returnValue = true;

            return returnValue;
        }


        /// <summary>
        /// Tests that the IndexOf function returns the expected result.
        /// </summary>
        /// <param name="list">An IList that is the list to test.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestIndexOf(IList list)
        {
            int index;
            bool returnValue = false;
            object newObject;

            newObject = new object();
            list.Add(newObject);
            index = list.IndexOf(newObject);
            if (list[index] == newObject)
                returnValue = true;

            return returnValue;
        }

        /// <summary>
        /// Tests that the Insert function returns the expected result.
        /// </summary>
        /// <param name="list">An IList that is the list to test.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestInsert(IList list)
        {
            bool returnValue = false;
            object newObject;

            newObject = new object();
            list.Add(new object());
            list.Add(new object());
            list.Add(new object());
            list.Insert(1, newObject);
            if (list[1] == newObject)
                returnValue = true;

            return returnValue;
        }

        /// <summary>
        /// Tests that the Remove function returns the expected result.
        /// </summary>
        /// <param name="list">An IList that is the list to test.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestRemove(IList list)
        {
            int baseCount;
            object[] objects = new object[] { new object(), new object(), new object() };
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
        /// <param name="list">An IList that is the list to test.</param>
        /// <returns>A bool indicating the result.</returns>
        public static bool TestRemoveAt(IList list)
        {
            int baseCount;
            bool returnValue = false;
            object newObject;

            newObject = new object();
            list.Add(new object());
            list.Add(new object());
            list.Add(new object());
            list.Insert(1, newObject);
            baseCount = list.Count;
            if (list[1] == newObject)
            {
                list.RemoveAt(1);
                if (list.Count == baseCount - 1 && list[1] != newObject)
                    returnValue = true;
            }

            return returnValue;
        }

        #endregion
        #endregion
    }
}