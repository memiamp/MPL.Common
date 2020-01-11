using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.TestHelpers;
using System;
using System.Security.Principal;

namespace MPL.Common.Security.Principal
{
    [TestClass]
    public class IPrincipalExtensionsTests
    {
        [TestMethod]
        public void ExtensionMethod_GetUserName_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(IPrincipal), typeof(IPrincipalExtensions), nameof(IPrincipalExtensions.GetUserName)));
        }

        [TestMethod]
        public void ExtensionMethod_GetUserPrincipal_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(IPrincipal), typeof(IPrincipalExtensions), nameof(IPrincipalExtensions.GetUserPrincipal)));
        }
    }
}