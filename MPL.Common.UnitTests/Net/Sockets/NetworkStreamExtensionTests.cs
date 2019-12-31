using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.TestHelpers;
using System;
using System.Net.Sockets;

namespace MPL.Common.Net.Sockets
{
    [TestClass]
    public class NetworkStreamExtensionsTests
    {
        [TestMethod]
        public void ExtensionMethod_ReadAvailable_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(NetworkStream), typeof(NetworkStreamExtensions), nameof(NetworkStreamExtensions.ReadAvailable)));
        }
    }
}