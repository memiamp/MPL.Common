using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.Collections.Concurrent;
using MPL.Common.TestHelpers;
using System;

namespace MPL.Common.Collections.Concurrent
{
    [TestClass]
    public class ConcurrentListTests
    {
        [TestMethod]
        public void List_StandardOperation_IsValid()
        {
            ConcurrentList<string> theList;

            theList = new ConcurrentList<string>();
            Assert.IsTrue(ListTestHelper<string>.TestAll(theList, () => { return TestDataTestHelper.GetString(); }));
        }

        [TestMethod]
        public void ListOperation_AddRemoveItems_IsValidCount()
        {
            ConcurrentList<string> theList;

            theList = new ConcurrentList<string>();
            Assert.AreEqual(0, theList.Count);

            for (int i = 0; i < 3; i++)
            {
                theList.Add(TestDataTestHelper.GetString());
                Assert.AreEqual(i + 1, theList.Count);
            }

            for (int i = theList.Count; i > 0; i--)
            {
                theList.Remove(theList[i - 1]);
                Assert.AreEqual(i - 1, theList.Count);
            }
        }

        [TestMethod]
        public void ObjectCreation_CreateWithValues_ValuesAreValid()
        {
            ConcurrentList<string> theList;
            string[] values;

            values = TestDataTestHelper.GetStrings();

            theList = new ConcurrentList<string>();
            for (int i = 0; i < values.Length; i++)
                theList.Add(values[i]);

            Assert.AreEqual(values.Length, theList.Count);
            for (int i = 0; i < values.Length; i++)
                Assert.IsTrue(theList.Contains(values[i]));
        }

        [TestMethod]
        public void ObjectCreation_CreateWithValues_ValuesExist()
        {
            ConcurrentList<string> theList;

            theList = new ConcurrentList<string>();
            for (int i = 0; i < 6; i++)
                theList.Add(TestDataTestHelper.GetString());

            Assert.AreEqual(6, theList.Count);
        }
    }
}