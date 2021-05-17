using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.TestHelpers;
using System;

namespace MPL.Common
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void CountAllChar_EmptyString_ReturnsZero()
        {
            Assert.AreEqual("".CountAll('A'), 0);
        }

        [TestMethod]
        public void CountAllChar_MultiMatch_ReturnsMulti()
        {
            Assert.AreEqual("ABCDEABCDEABCDEABCDEABCDE".CountAll('A'), 5);
        }

        [TestMethod]
        public void CountAllChar_NoMatches_ReturnsZero()
        {
            Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXY".CountAll('Z'), 0);
        }

        [TestMethod]
        public void CountAllChar_NullString_ReturnsZero()
        {
            string testString = null;

            Assert.AreEqual(testString.CountAll('A'), 0);
        }

        [TestMethod]
        public void CountAllChar_OneMatch_ReturnsOne()
        {
            Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXYZ".CountAll('A'), 1);
        }


        [TestMethod]
        public void CountAllString_EmptyString_ReturnsZero()
        {
            Assert.AreEqual("".CountAll("AB"), 0);
        }

        [TestMethod]
        public void CountAllString_MultiMatch_ReturnsMulti()
        {
            Assert.AreEqual("ABCDEABCDEABCDEABCDEABCDE".CountAll("BCD"), 5);
        }

        [TestMethod]
        public void CountAllString_NoMatches_ReturnsZero()
        {
            Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXY".CountAll("AZ"), 0);
        }

        [TestMethod]
        public void CountAllString_NullString_ReturnsZero()
        {
            string testString = null;

            Assert.AreEqual(testString.CountAll("AB"), 0);
        }

        [TestMethod]
        public void CountAllString_OneMatch_ReturnsOne()
        {
            Assert.AreEqual("ABCDEFGHIJKLMNOPQRSTUVWXYZ".CountAll("IJKL"), 1);
        }

        [TestMethod]
        public void StringExtensions_CountAllChar_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(string), typeof(StringExtensions), nameof(StringExtensions.CountAll), new[] { typeof(char) }));
        }

        [TestMethod]
        public void StringExtensions_CountAllString_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(string), typeof(StringExtensions), nameof(StringExtensions.CountAll), new[] { typeof(string) }));
        }
    }
}