using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.TestHelpers;
using System;

namespace MPL.Common
{
    [TestClass]
    public class ByteExtensionsTests
    {
        [TestMethod]
        public void ByteExtensions_EqualTo_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(object[]), typeof(ByteExtensions), nameof(ByteExtensions.EqualTo), new[] { typeof(byte[]) }));
        }

        [TestMethod]
        public void ByteExtensions_ToHexString_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(object[]), typeof(ByteExtensions), nameof(ByteExtensions.ToHexString), new[] { typeof(string), typeof(bool) }));
        }

        [TestMethod]
        public void EqualTo_DifferentLengths_ReturnsFalse()
        {
            byte[] dataA;
            byte[] dataB;

            dataA = TestDataTestHelper.GetBytes(10);
            dataB = TestDataTestHelper.GetBytes(11);

            Assert.IsFalse(dataA.EqualTo(dataB));
        }

        [TestMethod]
        public void EqualTo_DifferentValues_ReturnsFalse()
        {
            byte[] dataA;
            byte[] dataB;

            dataA = TestDataTestHelper.GetBytes(10);
            dataB = TestDataTestHelper.GetBytes(10);

            Assert.IsFalse(dataA.EqualTo(dataB));
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void EqualTo_NullParam1_ThrowsException()
        {
            byte[] dataA;
            byte[] dataB;

            dataA = null;
            dataB = TestDataTestHelper.GetBytes();

            dataA.EqualTo(dataB);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void EqualTo_NullParam2_ThrowsException()
        {
            byte[] dataA;
            byte[] dataB;

            dataA = TestDataTestHelper.GetBytes();
            dataB = null;

            dataA.EqualTo(dataB);
        }

        [TestMethod]
        public void EqualTo_ValidValues_ReturnsTrue()
        {
            byte[] dataA;
            byte[] dataB;

            dataA = TestDataTestHelper.GetBytes();
            dataB = dataA;

            Assert.IsTrue(dataA.EqualTo(dataB));
        }

        [TestMethod]
        public void ToHexString_DifferentDelimiter_OutputCorrect()
        {
            byte[] byteArray;

            byteArray = new byte[] { 00, 01, 02, 03, 04 };
            Assert.AreEqual("00X01X02X03X04", byteArray.ToHexString("X"));
        }

        [TestMethod]
        public void ToHexString_InvalidDelimiter_OutputCorrect()
        {
            byte[] byteArray;

            byteArray = new byte[] { 00, 01, 02, 03, 04 };
            Assert.AreEqual("0001020304", byteArray.ToHexString(null));
        }


        [TestMethod]
        public void ToHexString_InvalidValues_OutputCorrect()
        {
            byte[] byteArray;

            byteArray = new byte[0];
            Assert.AreEqual(string.Empty, byteArray.ToHexString());
        }

        [TestMethod]
        public void ToHexString_LowerCase_OutputCorrect()
        {
            byte[] byteArray;

            byteArray = new byte[] { 10, 11, 12, 13, 14 };
            Assert.AreEqual("0a 0b 0c 0d 0e", byteArray.ToHexString());
        }

        [TestMethod]
        public void ToHexString_UpperCase_OutputCorrect()
        {
            byte[] byteArray;

            byteArray = new byte[] { 10, 11, 12, 13, 14 };
            Assert.AreEqual("0A 0B 0C 0D 0E", byteArray.ToHexString(useLowerCase: false));
        }

        [TestMethod]
        public void ToHexString_ValidValues_OutputCorrect()
        {
            byte[] byteArray;

            byteArray = new byte[] { 00, 01, 02, 03, 04 };
            Assert.AreEqual("00 01 02 03 04", byteArray.ToHexString());
        }
    }
}