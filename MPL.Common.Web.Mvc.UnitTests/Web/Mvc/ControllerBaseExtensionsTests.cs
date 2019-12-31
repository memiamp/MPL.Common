using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.TestHelpers;

namespace MPL.Common.Web.Mvc
{
    [TestClass]
    public class ControllerBaseExtensionsTests
    {
        [TestMethod]
        public void ExtensionMethod_GetModelDisplayName_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(ControllerBase), typeof(ControllerBaseExtensions), nameof(ControllerBaseExtensions.GetModelDisplayName)));
        }
    }
}