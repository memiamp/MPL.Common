using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.TestHelpers;
using System;
using System.Threading;

namespace MPL.Common.Threading
{
    [TestClass]
    public class CancellationTokenExtensionsTests
    {
        [TestMethod]
        public void ExtensionMethod_AsTask_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(CancellationToken), typeof(CancellationTokenExtensions), nameof(CancellationTokenExtensions.AsTask)));
        }
    }
}