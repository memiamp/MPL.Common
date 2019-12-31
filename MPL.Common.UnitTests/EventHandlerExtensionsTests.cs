using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.TestHelpers;
using System;

namespace MPL.Common
{
    [TestClass]
    public class EventHandlerExtensionsTest
    {
        [TestMethod]
        public void ExtensionMethod_EventHandlerInvokeAsync_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(EventHandler), typeof(EventHandlerExtensions), nameof(EventHandlerExtensions.InvokeAsync), new[] { typeof(object) }));
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(EventHandler), typeof(EventHandlerExtensions), nameof(EventHandlerExtensions.InvokeAsync), new[] { typeof(object), typeof(EventArgs) }));
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(EventHandler), typeof(EventHandlerExtensions), nameof(EventHandlerExtensions.InvokeAsync), new[] { typeof(object), typeof(Func<EventArgs>) }));
        }
    }
}