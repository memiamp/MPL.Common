using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.ComponentModel;
using MPL.Common.TestHelpers;
using System;

namespace MPL.Common.ComponentModel
{
    [TestClass]
    public class LocalizedDisplayNameAttributeTests
    {
        [TestMethod]
        public void GetDisplayName_InvalidResourceName_ReturnsNull()
        {
            LocalizedDisplayNameAttribute attribute;

            attribute = new LocalizedDisplayNameAttribute
            {
                Name = TestDataTestHelper.GetString(),
                ResourceType = typeof(LocalizedDisplayNameAttributeTestsResourceProvider)
            };
            Assert.IsNull(attribute.DisplayName);
        }

        [TestMethod]
        public void GetDisplayName_UpdateResourceValueAfterDisplayName_CheckDisplayNameDoesntChange()
        {
            LocalizedDisplayNameAttribute attribute;
            string oldResourceValue;

            attribute = new LocalizedDisplayNameAttribute
            {
                Name = nameof(LocalizedDisplayNameAttributeTestsResourceProvider.ResourceName),
                ResourceType = typeof(LocalizedDisplayNameAttributeTestsResourceProvider)
            };
            oldResourceValue = LocalizedDisplayNameAttributeTestsResourceProvider.ResourceName;
            Assert.AreEqual(attribute.DisplayName, oldResourceValue);
            LocalizedDisplayNameAttributeTestsResourceProvider.ResourceName = TestDataTestHelper.GetString();
            Assert.AreEqual(attribute.DisplayName, oldResourceValue);
            Assert.AreNotEqual(attribute.DisplayName, LocalizedDisplayNameAttributeTestsResourceProvider.ResourceName);
        }

        [TestMethod]
        public void GetDisplayName_ValidInput_IsValid()
        {
            LocalizedDisplayNameAttribute attribute;

            attribute = new LocalizedDisplayNameAttribute
            {
                Name = nameof(LocalizedDisplayNameAttributeTestsResourceProvider.ResourceName),
                ResourceType = typeof(LocalizedDisplayNameAttributeTestsResourceProvider)
            };
            Assert.AreEqual(attribute.DisplayName, LocalizedDisplayNameAttributeTestsResourceProvider.ResourceName);
        }
    }
}