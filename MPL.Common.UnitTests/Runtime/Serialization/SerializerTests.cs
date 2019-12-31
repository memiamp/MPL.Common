using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.Runtime.Serialization;
using System;
using System.Dynamic;
using System.Text;

namespace MPL.Common.UnitTests.Runtime.Serialization
{
    [TestClass]
    public class SerializerTests
    {
        [TestMethod]
        public void SerialiseObject_ClassWithNamedMembers_IsValid()
        {
            string jsonString;
            SerialiserTestDummyClassNamed obj;

            obj = new SerialiserTestDummyClassNamed { TestA = "test value a", TestB = 17 };
            jsonString = Serializer.SerialiseToString(obj);
            Assert.AreEqual(jsonString, SerialiserTestDummyClassNamed.JsonOutput);
        }

        [TestMethod]
        public void SerialiseObject_ClassWithUnnamedMembers_IsValid()
        {
            string jsonString;
            SerialiserTestDummyClassUnnamed obj;

            obj = new SerialiserTestDummyClassUnnamed { TestA = "test value a", TestB = 17 };
            jsonString = Serializer.SerialiseToString(obj);
            Assert.AreEqual(jsonString, SerialiserTestDummyClassUnnamed.JsonOutput);
        }

        [TestMethod]
        public void SerialiseObject_Dynamic_IsValid()
        {
            string jsonString;
            dynamic obj = new ExpandoObject();

            obj.name = "test object";
            obj.value = "test value";

            jsonString = Serializer.SerialiseToString(obj);
            Assert.AreEqual(jsonString, "[{\"Key\":\"name\",\"Value\":\"test object\"},{\"Key\":\"value\",\"Value\":\"test value\"}]");
        }

        [TestMethod]
        public void TextEncoding_SetEncoding_SetsEcodingCorrectly()
        {
            Encoding encoding;

            encoding = Serializer.Encoding;

            Serializer.Encoding = Encoding.ASCII;
            Assert.AreEqual(Encoding.ASCII, Serializer.Encoding);

            Serializer.Encoding = encoding;
            Assert.AreEqual(encoding, Serializer.Encoding);
        }
    }
}