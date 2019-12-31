using System;
using System.Collections.Concurrent;

namespace MPL.Common.Collections.Concurrent
{
    /// <summary>
    /// A class that defines extensions to a Concurrent Queue.
    /// </summary>
    public static class ConcurrentQueueExtensions
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Clears the queue.
        /// </summary>
        /// <typeparam name="T">A T that is the type of item in the queue.</typeparam>
        /// <param name="queue">A ConcurrentQueue of T to be cleared.</param>
        public static void Clear<T>(this ConcurrentQueue<T> queue)
        {
            while (!queue.IsEmpty)
                queue.TryDequeue(out _);
        }

        /// <summary>
        /// Resizes the queue to the size specified by removing the oldest entries until the maximum size is reached.
        /// </summary>
        /// <typeparam name="T">A T that is the type of item in the queue.</typeparam>
        /// <param name="queue">A ConcurrentQueue of T to be cleared.</param>
        /// <param name="maximumSize">An int indicating the maximum size of the queue.</param>
        public static void Resize<T>(this ConcurrentQueue<T> queue, int maximumSize)
        {
            while (queue.Count > maximumSize)
                queue.TryDequeue(out _);
        }

        #endregion
        #endregion
    }
}