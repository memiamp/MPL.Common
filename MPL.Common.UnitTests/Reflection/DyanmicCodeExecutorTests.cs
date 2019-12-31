using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.Reflection;
using MPL.Common.TestHelpers;
using System;

namespace MPL.Common.UnitTests.Reflection
{
    [TestClass]
    public class DynamicCodeExecutorTest
    {
        [TestMethod]
        public void TryGetDynamicCodeOutput_InvalidCode_IsValidResult()
        {
            bool result;

            result = DynamicCodeExecutor.TryGetDynamicCodeOutput($"{TestDataTestHelper.GetString()}", out object returnedObject, out string warningText);
            Assert.IsFalse(result);
            Assert.IsNull(returnedObject);
            Assert.IsNotNull(warningText);
        }

        [TestMethod]
        public void TryGetDynamicCodeOutput_ValidCode_IsValidResult()
        {
            bool result;
            string testText;

            testText = TestDataTestHelper.GetString();
            result = DynamicCodeExecutor.TryGetDynamicCodeOutput($"\"{testText}\"", out object returnedObject, out string warningText);
            Assert.IsTrue(result);
            Assert.IsNotNull(returnedObject);
            Assert.IsNull(warningText);
            Assert.IsInstanceOfType(returnedObject, typeof(string));
            Assert.AreEqual(returnedObject.ToString(), testText);
        }
    }
}