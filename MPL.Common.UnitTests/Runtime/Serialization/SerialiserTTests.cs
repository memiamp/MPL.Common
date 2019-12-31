using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.Runtime.Serialization;
using System;
using System.IO;
using System.Text;

namespace MPL.Common.Runtime.Serialization
{
    [TestClass]
    public class SerialiserTTests
    {
        [TestMethod]
        public void DeserialiseObjectFromData_ClassWithNamedMembers_IsValid()
        {
            byte[] jsonData;
            SerialiserTestDummyClassNamed obj;

            jsonData = Serializer<SerialiserTestDummyClassNamed>.Encoding.GetBytes(SerialiserTestDummyClassNamed.JsonOutput);
            obj = Serializer<SerialiserTestDummyClassNamed>.Deserialise(jsonData);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.TestA);
            Assert.AreEqual(obj.TestA, "test value a");
            Assert.IsNotNull(obj.TestB);
            Assert.AreEqual(obj.TestB, 17);
        }

        [TestMethod]
        public void DeserialiseObjectFromData_ClassWithUnnamedMembers_IsValid()
        {
            byte[] jsonData;
            SerialiserTestDummyClassUnnamed obj;

            jsonData = Serializer<SerialiserTestDummyClassUnnamed>.Encoding.GetBytes(SerialiserTestDummyClassUnnamed.JsonOutput);
            obj = Serializer<SerialiserTestDummyClassUnnamed>.Deserialise(jsonData);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.TestA);
            Assert.AreEqual(obj.TestA, "test value a");
            Assert.IsNotNull(obj.TestB);
            Assert.AreEqual(obj.TestB, 17);
        }

        [TestMethod]
        public void DeserialiseObjectFromStream_ClassWithNamedMembers_IsValid()
        {
            byte[] jsonData;
            SerialiserTestDummyClassNamed obj;

            jsonData = Serializer<SerialiserTestDummyClassNamed>.Encoding.GetBytes(SerialiserTestDummyClassNamed.JsonOutput);
            using (MemoryStream memoryStream = new MemoryStream(jsonData))
            {
                obj = Serializer<SerialiserTestDummyClassNamed>.Deserialise(memoryStream);
            }
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.TestA);
            Assert.AreEqual(obj.TestA, "test value a");
            Assert.IsNotNull(obj.TestB);
            Assert.AreEqual(obj.TestB, 17);
        }

        [TestMethod]
        public void DeserialiseObjectFromStream_ClassWithUnnamedMembers_IsValid()
        {
            byte[] jsonData;
            SerialiserTestDummyClassUnnamed obj;

            jsonData = Serializer<SerialiserTestDummyClassUnnamed>.Encoding.GetBytes(SerialiserTestDummyClassUnnamed.JsonOutput);
            using (MemoryStream memoryStream = new MemoryStream(jsonData))
            {
                obj = Serializer<SerialiserTestDummyClassUnnamed>.Deserialise(memoryStream);
            }
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.TestA);
            Assert.AreEqual(obj.TestA, "test value a");
            Assert.IsNotNull(obj.TestB);
            Assert.AreEqual(obj.TestB, 17);
        }

        [TestMethod]
        public void DeserialiseObjectFromString_ClassWithNamedMembers_IsValid()
        {
            SerialiserTestDummyClassNamed obj;

            obj = Serializer<SerialiserTestDummyClassNamed>.Deserialise(SerialiserTestDummyClassNamed.JsonOutput);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.TestA);
            Assert.AreEqual(obj.TestA, "test value a");
            Assert.IsNotNull(obj.TestB);
            Assert.AreEqual(obj.TestB, 17);
        }

        [TestMethod]
        public void DeserialiseObjectFromString_ClassWithUnnamedMembers_IsValid()
        {
            SerialiserTestDummyClassUnnamed obj;

            obj = Serializer<SerialiserTestDummyClassUnnamed>.Deserialise(SerialiserTestDummyClassUnnamed.JsonOutput);
            Assert.IsNotNull(obj);
            Assert.IsNotNull(obj.TestA);
            Assert.AreEqual(obj.TestA, "test value a");
            Assert.IsNotNull(obj.TestB);
            Assert.AreEqual(obj.TestB, 17);
        }

        [TestMethod]
        public void SerialiseObjectToData_ClassWithNamedMembers_IsValid()
        {
            byte[] jsonData;
            SerialiserTestDummyClassNamed obj;

            obj = new SerialiserTestDummyClassNamed { TestA = "test value a", TestB = 17 };
            jsonData = Serializer<SerialiserTestDummyClassNamed>.Serialise(obj);
            CollectionAssert.AreEqual(jsonData, Serializer<SerialiserTestDummyClassNamed>.Encoding.GetBytes(SerialiserTestDummyClassNamed.JsonOutput));
        }

        [TestMethod]
        public void SerialiseObjectToData_ClassWithUnnamedMembers_IsValid()
        {
            byte[] jsonData;
            SerialiserTestDummyClassUnnamed obj;

            obj = new SerialiserTestDummyClassUnnamed { TestA = "test value a", TestB = 17 };
            jsonData = Serializer<SerialiserTestDummyClassUnnamed>.Serialise(obj);
            CollectionAssert.AreEqual(jsonData, Serializer<SerialiserTestDummyClassUnnamed>.Encoding.GetBytes(SerialiserTestDummyClassUnnamed.JsonOutput));
        }

        [TestMethod]
        public void SerialiseObjectToString_ClassWithNamedMembers_IsValid()
        {
            string jsonString;
            SerialiserTestDummyClassNamed obj;

            obj = new SerialiserTestDummyClassNamed { TestA = "test value a", TestB = 17 };
            jsonString = Serializer<SerialiserTestDummyClassNamed>.SerialiseToString(obj);
            Assert.AreEqual(jsonString, SerialiserTestDummyClassNamed.JsonOutput);
        }

        [TestMethod]
        public void SerialiseObjectToString_ClassWithUnnamedMembers_IsValid()
        {
            string jsonString;
            SerialiserTestDummyClassUnnamed obj;

            obj = new SerialiserTestDummyClassUnnamed { TestA = "test value a", TestB = 17 };
            jsonString = Serializer<SerialiserTestDummyClassUnnamed>.SerialiseToString(obj);
            Assert.AreEqual(jsonString, SerialiserTestDummyClassUnnamed.JsonOutput);
        }

        [TestMethod]
        public void TextEncoding_SetEncoding_SetsEcodingCorrectly()
        {
            Encoding encoding;

            encoding = Serializer<SerialiserTestDummyClassNamed>.Encoding;

            Serializer<SerialiserTestDummyClassNamed>.Encoding = Encoding.ASCII;
            Assert.AreEqual(Encoding.ASCII, Serializer<SerialiserTestDummyClassNamed>.Encoding);

            Serializer<SerialiserTestDummyClassNamed>.Encoding = encoding;
            Assert.AreEqual(encoding, Serializer<SerialiserTestDummyClassNamed>.Encoding);
        }
    }
}