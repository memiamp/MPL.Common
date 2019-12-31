using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.TestHelpers;
using MPL.Common.Windows.Forms;
using System;
using System.Windows.Forms;

namespace MPL.Common.Windows.UnitTests.Windows.Forms
{
    [TestClass]
    public class ControlExtensionsTest
    {
        [TestMethod]
        public void ExtensionMethod_BeginUpdate_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(Control), typeof(ControlExtensions), nameof(ControlExtensions.BeginUpdate)));
        }

        [TestMethod]
        public void ExtensionMethod_EndUpdate_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(Control), typeof(ControlExtensions), nameof(ControlExtensions.EndUpdate)));
        }
    }
}
