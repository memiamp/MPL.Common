using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.TestHelpers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MPL.Common.Threading.Tasks
{
    [TestClass]
    public class TaskExtensionsTests
    {
        [TestMethod]
        public void ExtensionMethod_WithWaitCancellation_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(Task<object>), typeof(TaskExtensions), nameof(TaskExtensions.WithWaitCancellation), new[] { typeof(CancellationToken) }));
        }
    }
}