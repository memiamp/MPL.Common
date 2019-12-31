using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.TestHelpers;

namespace MPL.Common.Web.Mvc
{
    [TestClass]
    public class EnumExtensionsTests
    {
        [TestMethod]
        public void ExtensionMethod_GetEnumDisplayName_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(Enum), typeof(EnumExtensions), nameof(EnumExtensions.GetEnumDisplayName)));
        }

        [TestMethod]
        public void GetEnumDisplayName_ValidEnumValueWithDisplayName_HasDisplayName()
        {
            EnumExtensionTestEnum theEnum;
            string result;

            theEnum = EnumExtensionTestEnum.ValueWithDisplayName;
            result = theEnum.GetEnumDisplayName();
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "EnumName");
        }

        [TestMethod]
        public void GetEnumDisplayName_ValidEnumValueWithoutDisplayName_DoesNotHaveDisplayName()
        {
            EnumExtensionTestEnum theEnum;
            string result;

            theEnum = EnumExtensionTestEnum.ValueWithoutDisplayName;
            result = theEnum.GetEnumDisplayName();
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "ValueWithoutDisplayName");
        }

        public enum EnumExtensionTestEnum : int
        {
            [Display(Name = "EnumName")]
            ValueWithDisplayName,

            ValueWithoutDisplayName
        }
    }
}