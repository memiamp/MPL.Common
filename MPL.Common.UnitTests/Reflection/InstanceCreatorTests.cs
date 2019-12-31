using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.Reflection;
using System;
using System.Reflection;

namespace MPL.Common.Reflection
{
    [TestClass]
    public class InstanceCreatorTests
    {
        private const string cCLASS_FULL_NAME = "MPL.Common.Reflection.InstanceCreatorTests";
        private const string cCLASS_INVALID_NAME = "b8a61df7fc984040bda1712265de741e";

        [TestMethod]
        public void CreateInstance_InvalidAssembly_IsNotFound()
        {
            try
            {
                InstanceCreatorTests createdObject;

                createdObject = InstanceCreator<InstanceCreatorTests>.CreateInstance(cCLASS_INVALID_NAME, cCLASS_FULL_NAME);
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains($"Unable to locate the type '{cCLASS_FULL_NAME}'"))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void CreateInstance_InvalidType_IsNotFound()
        {
            try
            {
                InstanceCreatorTests createdObject;

                createdObject = InstanceCreator<InstanceCreatorTests>.CreateInstance(Assembly.GetExecutingAssembly().FullName, cCLASS_INVALID_NAME);
            }
            catch (Exception ex)
            {
                if (!ex.Message.Contains($"Unable to locate the type '{cCLASS_INVALID_NAME}'"))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void CreateInstance_ValidType_IsFound()
        {
            try
            {
                InstanceCreatorTests createdObject;

                createdObject = InstanceCreator<InstanceCreatorTests>.CreateInstance(Assembly.GetExecutingAssembly().FullName, cCLASS_FULL_NAME);

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