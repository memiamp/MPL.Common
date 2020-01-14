using System;
using System.Collections;
using System.Collections.Generic;

namespace MPL.Common.Collections.Concurrent
{
    /// <summary>
    /// A class that defines a synchronised single-instance list of the specified type.
    /// </summary>
    /// <typeparam name="T">A T that is the type that is contained by the list.</typeparam>
    public class ConcurrentList<T> : IConcurrentList<T>
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public ConcurrentList()
        {
            _InternalList = ArrayList.Synchronized(new ArrayList());
            _ListInterface = this;
        }

        #endregion

        #region Declarations
        #region _Members_
        private readonly ArrayList _InternalList;
        private readonly IList<T> _ListInterface;

        #endregion
        #endregion

        #region Methods
        #region _Public_
        /// <summary>
        /// Adds an item to the list.
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            _ListInterface.Add(item);
        }

        /// <summary>
        /// Clears the list.
        /// </summary>
        public void Clear()
        {
            _ListInterface.Clear();
        }

        /// <summary>
        /// Gets an indication of whether the list contains the specified item.
        /// </summary>
        /// <param name="item">A T that is the item to verify in the list.</param>
        /// <returns>A bool indicating the result.</returns>
        public bool Contains(T item)
        {
            return _ListInterface.Contains(item);
        }

        /// <summary>
        /// Copies the list to the specified array, starting at the specified index.
        /// </summary>
        /// <param name="array">An array of T that is the array to copy the list to.</param>
        /// <param name="arrayIndex">An int indicating the starting position in the array for the copy.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _ListInterface.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Enumerates the list.
        /// </summary>
        /// <returns>An IEnumerator of type T containing the enumeration.</returns>
        IEnumerator<T> GetEnumerator()
        {
            IEnumerator Iterator;

            Iterator = _ListInterface.GetEnumerator();
            while (Iterator.MoveNext())
                yield return (T)Iterator.Current;
        }

        /// <summary>
        /// Gets the index of the specified item in the list.
        /// </summary>
        /// <param name="item">A T that is the item to get the index of.</param>
        /// <returns>An int indicating the index of the item.</returns>
        public int IndexOf(T item)
        {
            return _ListInterface.IndexOf(item);
        }

        /// <summary>
        /// Inserts an item into the list at the specified index.
        /// </summary>
        /// <param name="index">An int indicating the index at which to insert the item.</param>
        /// <param name="item">A T that is the item to be inserted.</param>
        public void Insert(int index, T item)
        {
            _ListInterface.Insert(index, item);
        }

        /// <summary>
        /// Removes the specified item from the list.
        /// </summary>
        /// <param name="item">A T that is the item to remove.</param>
        /// <returns>A bool indicating whether the item was removed.</returns>
        public bool Remove(T item)
        {
            return _ListInterface.Remove(item);
        }

        /// <summary>
        /// Removes the item from the list at the specified index.
        /// </summary>
        /// <param name="index">An int indicating the index of the item to remove.</param>
        public void RemoveAt(int index)
        {
            _ListInterface.RemoveAt(index);
        }

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Access the item in the list at the specified index.
        /// </summary>
        /// <param name="index">An int indicating the index of the item to access.</param>
        /// <returns>A T that is the item at the specified index.</returns>
        public T this[int index]
        {
            get { return _ListInterface[index]; }
            set { _ListInterface[index] = value; }
        }

        /// <summary>
        /// Gets the count of items in the list.
        /// </summary>
        public int Count
        {
            get { return _ListInterface.Count; }
        }

        /// <summary>
        /// Gets an indication of whether the list is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return _ListInterface.IsReadOnly; }
        }

        #endregion

        #region Interfaces
        #region _ICollection<T>_
        #region __Methods__
        void ICollection<T>.Add(T item)
        {
            _InternalList.Add(item);
        }

        void ICollection<T>.Clear()
        {
            _InternalList.Clear();
        }

        bool ICollection<T>.Contains(T item)
        {
            return _InternalList.Contains(item);
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            _InternalList.CopyTo(array, arrayIndex);
        }

        bool ICollection<T>.Remove(T item)
        {
            bool ReturnValue = false;

            if (_InternalList.Contains(item))
            {
                _InternalList.Remove(item);
                ReturnValue = _InternalList.Contains(item);
            }

            return ReturnValue;
        }

        #endregion
        #region __Properties__
        int ICollection<T>.Count
        {
            get { return _InternalList.Count; }
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return _InternalList.IsReadOnly; }
        }

        #endregion
        #endregion
        #region _IEnumerator_
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _InternalList.GetEnumerator();
        }

        #endregion
        #region _IEnumerator<T>_
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            IEnumerator Iterator;

            Iterator = _InternalList.GetEnumerator();
            while (Iterator.MoveNext())
                yield return (T)Iterator.Current;
        }

        #endregion
        #region _IList_
        #region __Methods__
        int IList<T>.IndexOf(T item)
        {
            return _InternalList.IndexOf(item);
        }

        void IList<T>.Insert(int index, T item)
        {
            _InternalList.Insert(index, item);
        }

        void IList<T>.RemoveAt(int index)
        {
            _InternalList.RemoveAt(index);
        }

        #endregion
        #region __Properties__
        T IList<T>.this[int index]
        {
            get { return (T)_InternalList[index]; }
            set { _InternalList[index] = value; }
        }

        #endregion
        #endregion
        #endregion
    }
}