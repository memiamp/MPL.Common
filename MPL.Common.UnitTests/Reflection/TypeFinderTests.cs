using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.Reflection;
using System;
using System.Reflection;

namespace MPL.Common.UnitTests.Reflection
{
    [TestClass]
    public class TypeFinderTest
    {
        [TestMethod]
        public void TryFindType_InvalidAssembly_IsNotFound()
        {
            bool result;

            result = TypeFinder.TryFindType("b8a61df7fc984040bda1712265de741e", "MPL.Common.UnitTests.Reflection.TypeFinderTest", out Type type);
            Assert.IsFalse(result);
            Assert.IsNull(type);
        }

        [TestMethod]
        public void TryFindType_InvalidType_IsNotFound()
        {
            bool result;

            result = TypeFinder.TryFindType(Assembly.GetExecutingAssembly().FullName, "b8a61df7fc984040bda1712265de741e", out Type type);
            Assert.IsFalse(result);
            Assert.IsNull(type);
        }

        [TestMethod]
        public void TryFindType_ValidType_IsFound()
        {
            bool result;

            result = TypeFinder.TryFindType(Assembly.GetExecutingAssembly().FullName, "MPL.Common.UnitTests.Reflection.TypeFinderTest", out Type type);
            Assert.IsTrue(result);
            Assert.IsNotNull(type);
            Assert.AreEqual(type.FullName, "MPL.Common.UnitTests.Reflection.TypeFinderTest");
        }

        [TestMethod]
        public void TryLoadAssembly_InvalidAssembly_IsNotLoaded()
        {
            bool result;

            result = TypeFinder.TryLoadAssembly("b8a61df7fc984040bda1712265de741e", out Assembly assembly);
            Assert.IsFalse(result);
            Assert.IsNull(assembly);
        }

        [TestMethod]
        public void TryLoadAssembly_ValidAssembly_IsLoaded()
        {
            bool result;

            result = TypeFinder.TryLoadAssembly("MPL.Common.UnitTests", out Assembly assembly);
            Assert.IsTrue(result);
            Assert.IsNotNull(assembly);
            Assert.AreEqual(assembly.GetName().Name, "MPL.Common.UnitTests");
        }
    }
}