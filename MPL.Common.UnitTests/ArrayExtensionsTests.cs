using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.TestHelpers;
using System;

namespace MPL.Common
{
    [TestClass]
    public class ArrayExtensionsTests
    {
        [TestMethod]
        public void Append_AppendItem_ItemIsAppended()
        {
            string newValueA;
            string newValueB;
            string[] result;
            string[] values;

            values = TestDataTestHelper.GetStrings(arraySize: 5);
            newValueA = TestDataTestHelper.GetString();
            newValueB = TestDataTestHelper.GetString();

            Assert.AreEqual(5, values.Length);
            result = values.Append(newValueA);
            Assert.AreEqual(5, values.Length);
            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(newValueA, result[5]);

            result = result.Append(newValueB);
            Assert.AreEqual(7, result.Length);
            Assert.AreEqual(newValueB, result[6]);
        }

        [TestMethod]
        public void ExtensionMethod_Append_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(object[]), typeof(ArrayExtensions), nameof(ArrayExtensions.Append), new[] { typeof(object) }));
        }

        [TestMethod]
        public void ExtensionMethod_InsertAt_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(object[]), typeof(ArrayExtensions), nameof(ArrayExtensions.InsertAt), new[] { typeof(int), typeof(object) }));
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(object[]), typeof(ArrayExtensions), nameof(ArrayExtensions.InsertAt), new[] { typeof(int), typeof(object[]) }));
        }

        [TestMethod]
        public void InsertAt_InsertItemAtEnd_ItemIsInserted()
        {
            string newValue;
            string[] result;
            string[] values;

            values = TestDataTestHelper.GetStrings(arraySize: 5);
            newValue = TestDataTestHelper.GetString();

            Assert.AreEqual(5, values.Length);
            result = values.InsertAt(5, newValue);

            Assert.AreEqual(5, values.Length);
            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(newValue, result[5]);
            for (int i = 0; i < 5; i++)
                Assert.AreEqual(result[i], values[i]);
        }

        [TestMethod]
        public void InsertAt_InsertItemAtStart_ItemIsInserted()
        {
            string newValue;
            string[] result;
            string[] values;

            values = TestDataTestHelper.GetStrings(arraySize: 5);
            newValue = TestDataTestHelper.GetString();

            Assert.AreEqual(5, values.Length);
            result = values.InsertAt(0, newValue);

            Assert.AreEqual(5, values.Length);
            Assert.AreEqual(6, result.Length);
            Assert.AreEqual(newValue, result[0]);
            for (int i = 0; i < 5; i++)
                Assert.AreEqual(result[i + 1], values[i]);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertAt_InsertItemIndexTooHigh_ArgumentExceptionIsThrown()
        {
            string[] values;

            values = TestDataTestHelper.GetStrings(arraySize: 5);

            Assert.AreEqual(5, values.Length);
            values = values.InsertAt(6, TestDataTestHelper.GetString());
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertAt_InsertItemIndexTooLow_ArgumentExceptionIsThrown()
        {
            string[] values;

            values = TestDataTestHelper.GetStrings(arraySize: 5);

            Assert.AreEqual(5, values.Length);
            values = values.InsertAt(-1, TestDataTestHelper.GetString());
        }

        [TestMethod]
        public void InsertAt_InsertItemInMiddle_ItemIsInserted()
        {
            string newValueA;
            string newValueB;
            string[] result;
            string[] values;

            values = TestDataTestHelper.GetStrings(arraySize: 5);
            newValueA = TestDataTestHelper.GetString();
            newValueB = TestDataTestHelper.GetString();

            Assert.AreEqual(5, values.Length);
            result = values.InsertAt(2, newValueA);

            Assert.AreEqual(values[0], result[0]);
            Assert.AreEqual(values[1], result[1]);
            Assert.AreEqual(newValueA, result[2]);
            Assert.AreEqual(values[2], result[3]);
            Assert.AreEqual(values[3], result[4]);
            Assert.AreEqual(values[4], result[5]);

            result = result.InsertAt(4, newValueB);
            Assert.AreEqual(values[0], result[0]);
            Assert.AreEqual(values[1], result[1]);
            Assert.AreEqual(newValueA, result[2]);
            Assert.AreEqual(values[2], result[3]);
            Assert.AreEqual(newValueB, result[4]);
            Assert.AreEqual(values[3], result[5]);
            Assert.AreEqual(values[4], result[6]);
        }

        [TestMethod]
        public void InsertAt_InsertItemsAtEnd_ItemsAreInserted()
        {
            string[] newValues;
            string[] result;
            string[] values;

            values = TestDataTestHelper.GetStrings(arraySize: 5);
            newValues = TestDataTestHelper.GetStrings(arraySize: 3);

            Assert.AreEqual(5, values.Length);
            Assert.AreEqual(3, newValues.Length);
            result = values.InsertAt(5, newValues);

            Assert.AreEqual(5, values.Length);
            Assert.AreEqual(3, newValues.Length);
            Assert.AreEqual(8, result.Length);
            for (int i = 0; i < 5; i++)
                Assert.AreEqual(result[i], values[i]);
            for (int i = 0; i < newValues.Length; i++)
                Assert.AreEqual(result[5 + i], newValues[i]);
        }

        [TestMethod]
        public void InsertAt_InsertItemsAtStart_ItemsAreInserted()
        {
            string[] newValues;
            string[] result;
            string[] values;

            values = TestDataTestHelper.GetStrings(arraySize: 5);
            newValues = TestDataTestHelper.GetStrings(arraySize: 3);

            Assert.AreEqual(5, values.Length);
            Assert.AreEqual(3, newValues.Length);
            result = values.InsertAt(0, newValues);

            Assert.AreEqual(5, values.Length);
            Assert.AreEqual(3, newValues.Length);
            Assert.AreEqual(8, result.Length);
            for (int i = 0; i < 3; i++)
                Assert.AreEqual(result[i], newValues[i]);
            for (int i = 0; i < 5; i++)
                Assert.AreEqual(result[i + 3], values[i]);
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertAt_InsertItemsIndexTooHigh_ArgumentExceptionIsThrown()
        {
            string[] values;

            values = TestDataTestHelper.GetStrings(arraySize: 5);

            Assert.AreEqual(5, values.Length);
            values = values.InsertAt(6, TestDataTestHelper.GetStrings());
        }

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void InsertAt_InsertItemsIndexTooLow_ArgumentExceptionIsThrown()
        {
            string[] values;

            values = TestDataTestHelper.GetStrings(arraySize: 5);

            Assert.AreEqual(5, values.Length);
            values = values.InsertAt(-1, TestDataTestHelper.GetStrings());
        }

        [TestMethod]
        public void InsertAt_InsertItesmInMiddle_ItemsAreInserted()
        {
            string[] newValuesA;
            string[] newValuesB;
            string[] result;
            string[] values;

            values = TestDataTestHelper.GetStrings(arraySize: 5);
            newValuesA = TestDataTestHelper.GetStrings(arraySize: 3);
            newValuesB = TestDataTestHelper.GetStrings(arraySize: 3);

            Assert.AreEqual(5, values.Length);
            Assert.AreEqual(3, newValuesA.Length);
            result = values.InsertAt(2, newValuesA);

            Assert.AreEqual(values[0], result[0]);
            Assert.AreEqual(values[1], result[1]);
            Assert.AreEqual(newValuesA[0], result[2]);
            Assert.AreEqual(newValuesA[1], result[3]);
            Assert.AreEqual(newValuesA[2], result[4]);
            Assert.AreEqual(values[2], result[5]);
            Assert.AreEqual(values[3], result[6]);
            Assert.AreEqual(values[4], result[7]);

            Assert.AreEqual(3, newValuesB.Length);
            result = result.InsertAt(4, newValuesB);
            Assert.AreEqual(values[0], result[0]);
            Assert.AreEqual(values[1], result[1]);
            Assert.AreEqual(newValuesA[0], result[2]);
            Assert.AreEqual(newValuesA[1], result[3]);
            Assert.AreEqual(newValuesB[0], result[4]);
            Assert.AreEqual(newValuesB[1], result[5]);
            Assert.AreEqual(newValuesB[2], result[6]);
            Assert.AreEqual(newValuesA[2], result[7]);
            Assert.AreEqual(values[2], result[8]);
            Assert.AreEqual(values[3], result[9]);
            Assert.AreEqual(values[4], result[10]);
        }

    }
}