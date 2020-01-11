using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.TestHelpers;
using System;
using System.Security.Principal;

namespace MPL.Common.Security.Principal
{
    [TestClass]
    public class IIdentityExtensionsTests
    {
        [TestMethod]
        public void ExtensionMethod_GetUserPrincipal_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(IIdentity), typeof(IIdentityExtensions), nameof(IIdentityExtensions.GetUserPrincipal)));
        }
    }
}