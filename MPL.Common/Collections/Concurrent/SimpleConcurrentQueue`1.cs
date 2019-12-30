using System;
using System.Collections;

namespace MPL.Common.Collections.Concurrent
{
    /// <summary>
    /// A class that defines a simple, thread-safe synchronized generic queue.
    /// </summary>
    /// <typeparam name="T">A T that is the type contained by the queue.</typeparam>
    public sealed class SimpleConcurrentQueue<T>
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public SimpleConcurrentQueue()
        {
            _Queue = Queue.Synchronized(new Queue());
        }

        #endregion

        #region Declarations
        #region _Members_
        private Queue _Queue;

        #endregion
        #endregion

        #region Methods
        #region _Public_
        /// <summary>
        /// Clears the items from the queue.
        /// </summary>
        public void Clear()
        {
            _Queue.Clear();
        }

        /// <summary>
        /// Gets an indication of whether the queue contains the specified object.
        /// </summary>
        /// <param name="obj">A T that is the object to check for in the queue.</param>
        /// <returns>A bool indicating whether the object is in the queue.</returns>
        public bool Contains(T obj)
        {
            return _Queue.Contains(obj);
        }

        /// <summary>
        /// Dequeues the next object from the queue.
        /// </summary>
        /// <returns>A T that is the dequeue object.</returns>
        public T Dequeue()
        {
            return (T)_Queue.Dequeue();
        }

        /// <summary>
        /// Enqueues the specified item.
        /// </summary>
        /// <param name="obj">A T that is the item to add to the end of the queue.</param>
        public void Enqueue(T obj)
        {
            _Queue.Enqueue(obj);
        }

        /// <summary>
        /// Peeks at the next item in the queue.
        /// </summary>
        /// <returns>A T that is the next item in the queue.</returns>
        public T Peek()
        {
            return (T)_Queue.Peek();
        }

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets the number of items in the queue.
        /// </summary>
        public int Count
        {
            get { return _Queue.Count; }
        }

        #endregion
    }
}