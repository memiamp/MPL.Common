using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace MPL.Common.Reflection
{
    [TestClass]
    public class TypeFinderTests
    {
        private const string cASSEMBLY_NAME = "MPL.Common.UnitTests";
        private const string cCLASS_FULL_NAME = "MPL.Common.Reflection.TypeFinderTests";
        private const string cCLASS_INVALID_NAME = "b8a61df7fc984040bda1712265de741e";

        [TestMethod]
        public void FindAllTypesOf_ValidAbstractType_FindsSingleType()
        {
            Type[] result;

            result = TypeFinder.FindAllTypesOf<TypeFinderAbstractBaseClass>();
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Length);
        }

        [TestMethod]
        public void FindAllTypesOf_ValidType_FindsTypes()
        {
            Type[] result;

            result = TypeFinder.FindAllTypesOf<TypeFinderBaseClass>();
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Length);
        }

        [TestMethod]
        public void TryFindType_InvalidAssembly_IsNotFound()
        {
            bool result;

            result = TypeFinder.TryFindType(cCLASS_INVALID_NAME, cCLASS_FULL_NAME, out Type type);
            Assert.IsFalse(result);
            Assert.IsNull(type);
        }

        [TestMethod]
        public void TryFindType_InvalidType_IsNotFound()
        {
            bool result;

            result = TypeFinder.TryFindType(Assembly.GetExecutingAssembly().FullName, cCLASS_INVALID_NAME, out Type type);
            Assert.IsFalse(result);
            Assert.IsNull(type);
        }

        [TestMethod]
        public void TryFindType_ValidType_IsFound()
        {
            bool result;

            result = TypeFinder.TryFindType(Assembly.GetExecutingAssembly().FullName, cCLASS_FULL_NAME, out Type type);
            Assert.IsTrue(result);
            Assert.IsNotNull(type);
            Assert.AreEqual(type.FullName, cCLASS_FULL_NAME);
        }

        [TestMethod]
        public void TryLoadAssembly_InvalidAssembly_IsNotLoaded()
        {
            bool result;

            result = TypeFinder.TryLoadAssembly(cCLASS_INVALID_NAME, out Assembly assembly);
            Assert.IsFalse(result);
            Assert.IsNull(assembly);
        }

        [TestMethod]
        public void TryLoadAssembly_ValidAssembly_IsLoaded()
        {
            bool result;

            result = TypeFinder.TryLoadAssembly(cASSEMBLY_NAME, out Assembly assembly);
            Assert.IsTrue(result);
            Assert.IsNotNull(assembly);
            Assert.AreEqual(assembly.GetName().Name, cASSEMBLY_NAME);
        }
    }
}