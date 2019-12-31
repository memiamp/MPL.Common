using MPL.Common.TestHelpers;
using System;

namespace MPL.Common.UnitTests.ComponentModel
{
    /// <summary>
    /// A class that defines a resource provider for testing of a localized display name attribute.
    /// </summary>
    public static class LocalizedDisplayNameAttributeTestsResourceProvider
    {
        #region Constructors
        static LocalizedDisplayNameAttributeTestsResourceProvider()
        {
            ResourceName = TestDataTestHelper.GetString();
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the resource name.
        /// </summary>
        public static string ResourceName { get; set; }

        #endregion
    }
}