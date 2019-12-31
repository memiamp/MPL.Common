using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.Collections.Specialized;
using MPL.Common.TestHelpers;
using System;
using System.Collections.Specialized;

namespace MPL.Common.UnitTests.Collections.Specialized
{
    [TestClass]
    public class StringCollectionExtensionsTests
    {
        [TestMethod]
        public void ExtensionMethod_ToArray_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(StringCollection), typeof(StringCollectionExtensions), nameof(StringCollectionExtensions.ToArray)));
        }

        [TestMethod]
        public void ExtensionMethod_ToArray_FunctionsWithData()
        {
            StringCollection collection;

            collection = new StringCollection
            {
                TestDataTestHelper.GetString(),
                TestDataTestHelper.GetString(),
                TestDataTestHelper.GetString(),
                TestDataTestHelper.GetString(),
                TestDataTestHelper.GetString()
            };
            Assert.AreEqual(5, collection.Count);
            Assert.AreEqual(5, collection.ToArray().Length);
        }

        [TestMethod]
        public void ExtensionMethod_ToArray_OrderedDataReturned()
        {
            StringCollection collection;
            string[] outputArray;
            string[] sourceData;

            collection = new StringCollection();

            sourceData = TestDataTestHelper.GetStrings();
            for (int i = 0; i < sourceData.Length; i++)
                collection.Add(sourceData[i]);

            outputArray = collection.ToArray();

            Assert.AreEqual(sourceData.Length, collection.Count);
            Assert.AreEqual(sourceData.Length, outputArray.Length);

            for (int i = 0; i < sourceData.Length; i++)
                Assert.AreEqual(outputArray[i], sourceData[i]);
        }
    }
}