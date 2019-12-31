using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.Collections.Concurrent;
using MPL.Common.TestHelpers;
using System;
using System.Collections.Concurrent;

namespace MPL.Common.UnitTests.Collections.Concurrent
{
    [TestClass]
    public class ConcurrentQueueExtensionsTests
    {
        [TestMethod]
        public void Clear_QueueWithItems_QueueIsCleared()
        {
            ConcurrentQueue<object> queue;

            queue = new ConcurrentQueue<object>();
            for (int i = 0; i < 5; i++)
                queue.Enqueue(new object());
            Assert.AreEqual(5, queue.Count);

            queue.Clear();
            Assert.AreEqual(0, queue.Count);
        }

        [TestMethod]
        public void ExtensionMethod_Clear_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(ConcurrentQueue<object>), typeof(ConcurrentQueueExtensions), nameof(ConcurrentQueueExtensions.Clear)));
        }

        [TestMethod]
        public void ExtensionMethod_Resize_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(ConcurrentQueue<object>), typeof(ConcurrentQueueExtensions), nameof(ConcurrentQueueExtensions.Resize), new[] { typeof(int) }));
        }

        [TestMethod]
        public void Resize_QueueWithItems_ItemsAreInValidOrder()
        {
            ConcurrentQueue<int> queue;

            queue = new ConcurrentQueue<int>();

            // Add 10 numbers: 0..9
            for (int i = 0; i < 10; i++)
                queue.Enqueue(i);
            Assert.AreEqual(10, queue.Count);

            // Resize to 8 numbers. Oldest two entries (0, 1) should be removed). Next number will be 2
            queue.Resize(8);
            Assert.AreEqual(8, queue.Count);
            Assert.IsTrue(queue.TryPeek(out int nextNumber));
            Assert.AreEqual(2, nextNumber);

            // Resize to 5 numbers. Oldest two entries (2, 3, 4) should be removed). Next number will be 5
            queue.Resize(5);
            Assert.AreEqual(5, queue.Count);
            Assert.IsTrue(queue.TryPeek(out nextNumber));
            Assert.AreEqual(5, nextNumber);

            // Resize to 2 numbers. Oldest two entries (5, 6, 7) should be removed). Next number will be 8
            queue.Resize(2);
            Assert.AreEqual(2, queue.Count);
            Assert.IsTrue(queue.TryPeek(out nextNumber));
            Assert.AreEqual(8, nextNumber);

            queue.Resize(0);
            Assert.AreEqual(0, queue.Count);
            Assert.IsFalse(queue.TryPeek(out nextNumber));
        }

        [TestMethod]
        public void Resize_QueueWithItems_ResizeCorrecetly()
        {
            ConcurrentQueue<object> queue;

            queue = new ConcurrentQueue<object>();

            for (int i = 0; i < 10; i++)
                queue.Enqueue(new object());
            Assert.AreEqual(10, queue.Count);

            queue.Resize(8);
            Assert.AreEqual(8, queue.Count);

            queue.Resize(5);
            Assert.AreEqual(5, queue.Count);

            queue.Resize(2);
            Assert.AreEqual(2, queue.Count);

            queue.Resize(0);
            Assert.AreEqual(0, queue.Count);
        }
    }
}