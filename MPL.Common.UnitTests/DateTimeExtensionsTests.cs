using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.TestHelpers;
using System;

namespace MPL.Common
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod]
        public void DateTimeExtensions_ToIso8601_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(DateTime), typeof(DateTimeExtensions), nameof(DateTimeExtensions.ToIso8601)));
        }

        [TestMethod]
        public void DateTimeExtensions_ToIso8601Z_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(DateTime), typeof(DateTimeExtensions), nameof(DateTimeExtensions.ToIso8601Z)));
        }

        [TestMethod]
        public void Conversion_ToIso8601_OutputCorrect()
        {
            DateTime dateTime = new DateTime(2020, 01, 25, 15, 58, 22);
            Assert.AreEqual("2020-01-25T15:58:22", dateTime.ToIso8601());
        }

        [TestMethod]
        public void Conversion_ToIso8601Z_OutputCorrect()
        {
            DateTime dateTime = new DateTime(2020, 01, 25, 15, 58, 22);
            Assert.AreEqual("2020-01-25T15:58:22Z", dateTime.ToIso8601Z());
        }
    }
}