using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.Reflection;
using System;
using System.Reflection;

namespace MPL.Common.UnitTests.Reflection
{
    [TestClass]
    public class InstanceCreatorTests
    {
        [TestMethod]
        public void CreateInstance_InvalidAssembly_IsNotFound()
        {
            try
            {
                InstanceCreatorTests createdObject;

                createdObject = InstanceCreator<InstanceCreatorTests>.CreateInstance("b8a61df7fc984040bda1712265de741e", "MPL.Common.UnitTests.Reflection.InstanceCreatorTests");
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("Unable to locate the type 'MPL.Common.UnitTests.Reflection.InstanceCreatorTests'"))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void CreateInstance_InvalidType_IsNotFound()
        {
            try
            {
                InstanceCreatorTests createdObject;

                createdObject = InstanceCreator<InstanceCreatorTests>.CreateInstance(Assembly.GetExecutingAssembly().FullName, "b8a61df7fc984040bda1712265de741e");
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains("Unable to locate the type 'b8a61df7fc984040bda1712265de741e'"))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void CreateInstance_ValidType_IsFound()
        {
            try
            {
                InstanceCreatorTests createdObject;

                createdObject = InstanceCreator<InstanceCreatorTests>.CreateInstance(Assembly.GetExecutingAssembly().FullName, "MPL.Common.UnitTests.Reflection.InstanceCreatorTests");

                Assert.IsNotNull(createdObject);
                Assert.IsInstanceOfType(createdObject, typeof(InstanceCreatorTests));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}