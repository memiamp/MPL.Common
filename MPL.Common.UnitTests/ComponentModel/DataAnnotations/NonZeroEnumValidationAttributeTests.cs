using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.ComponentModel.DataAnnotations;
using MPL.Common.TestHelpers;
using System;

namespace MPL.Common.ComponentModel.DataAnnotations
{
    [TestClass]
    public class NonZeroEnumValidationAttributeTests
    {
        [TestMethod]
        public void ValidateInput_InvalidEnumValue_IsInvalid()
        {
            NonZeroEnumValidationAttribute attribute;

            attribute = new NonZeroEnumValidationAttribute();
            Assert.IsFalse(attribute.IsValid(NonZeroEnumValidationAttributeTestsEnum.ZeroValue));
        }

        [TestMethod]
        public void ValidateInput_InvalidNonEnumValues_AreInvalid()
        {
            NonZeroEnumValidationAttribute attribute;

            attribute = new NonZeroEnumValidationAttribute();
            Assert.IsFalse(attribute.IsValid(0));
            Assert.IsFalse(attribute.IsValid(TestDataTestHelper.GetInt(1)));
            Assert.IsFalse(attribute.IsValid(TestDataTestHelper.GetString()));
            Assert.IsFalse(attribute.IsValid(new object()));
        }

        [TestMethod]
        public void ValidateInput_ValidInput_IsValid()
        {
            NonZeroEnumValidationAttribute attribute;

            attribute = new NonZeroEnumValidationAttribute();
            Assert.IsTrue(attribute.IsValid(NonZeroEnumValidationAttributeTestsEnum.NonZeroValueA));
            Assert.IsTrue(attribute.IsValid(NonZeroEnumValidationAttributeTestsEnum.NonZeroValueB));
        }

        public enum NonZeroEnumValidationAttributeTestsEnum : int
        {
            NonZeroValueA = 2,

            NonZeroValueB = 100,

            ZeroValue = 0
        }
    }
}