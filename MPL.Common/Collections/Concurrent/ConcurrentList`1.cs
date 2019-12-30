using System;
using System.Collections;
using System.Collections.Generic;

namespace MPL.Common.Collections.Concurrent
{
    /// <summary>
    /// A class that defines a thread-safe concurrent list of T.
    /// </summary>
    /// <typeparam name="T">A T that is the type contained by the list.</typeparam>
    public sealed class ConcurrentList<T> : IList<T>
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public ConcurrentList()
        {
            _List = ArrayList.Synchronized(new List<T>());
        }

        #endregion

        #region Declarations
        #region _Members_
        private readonly IList _List;

        #endregion
        #endregion

        #region Interfaces
        #region _IList<T>_
        #region __Methods__
        /// <summary>
        /// Adds an item to the list.
        /// </summary>
        /// <param name="item">A T that is the item to add.</param>
        public void Add(T item)
        {
            _List.Add(item);
        }

        /// <summary>
        /// Clears the list.
        /// </summary>
        public void Clear()
        {
            _List.Clear();
        }

        /// <summary>
        /// Gets an indication of whether the list contains the specified item.
        /// </summary>
        /// <param name="item">A T that is the item to check.</param>
        /// <returns>A bool that indicates the result.</returns>
        public bool Contains(T item)
        {
            return _List.Contains(item);
        }

        /// <summary>
        /// Copies the contents of the list from the specified position into the target array.
        /// </summary>
        /// <param name="array">An array of T that is the array to copy to.</param>
        /// <param name="arrayIndex">An int indicating the index of the first item to copy.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _List.CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets an enumerator for the list.
        /// </summary>
        /// <returns>An IEnumerator of type T for the list.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new ConcurrentEnumerator<T>(_List.GetEnumerator());
        }

        /// <summary>
        /// Gets the index of the specified item.
        /// </summary>
        /// <param name="item">A T that is the item to get the index of.</param>
        /// <returns>An int indicating the index of the item.</returns>
        public int IndexOf(T item)
        {
            return _List.IndexOf(item);
        }

        /// <summary>
        /// Inserts an item into the list at the specified index.
        /// </summary>
        /// <param name="index">An int indicating the position to insert the item at.</param>
        /// <param name="item">A T that is the item to insert.</param>
        public void Insert(int index, T item)
        {
            _List.Insert(index, item);
        }

        /// <summary>
        /// Removes the specified item from the list.
        /// </summary>
        /// <param name="item">A T that is the item to remove.</param>
        /// <returns>A bool indicating whether the item was removed.</returns>
        public bool Remove(T item)
        {
            bool ReturnValue = false;

            if (_List.Contains(item))
            {
                _List.Remove(item);
                ReturnValue = true;
            }

            return ReturnValue;
        }

        /// <summary>
        /// Removes the item from the list at the specified index.
        /// </summary>
        /// <param name="index">An int indicating the index to remove the item from.</param>
        public void RemoveAt(int index)
        {
            _List.RemoveAt(index);
        }

        #endregion
        #region __Properties__
        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index">An indicating the index of the item.</param>
        /// <returns>A T that is the item at the index.</returns>
        public T this[int index]
        {
            get { return (T)_List[index]; }
            set { _List[index] = value; }
        }

        /// <summary>
        /// Gets the count of items in the list.
        /// </summary>
        public int Count
        {
            get { return _List.Count; }
        }

        /// <summary>
        /// Gets an indication of whether the list is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return _List.IsReadOnly; }
        }

        #endregion
        #endregion
        #endregion
    }
}