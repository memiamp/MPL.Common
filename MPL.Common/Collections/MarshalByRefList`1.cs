using System;
using System.Collections;
using System.Collections.Generic;

namespace MPL.Common.Collections
{
    /// <summary>
    /// A generic collection class that is suitable for remote proxy consumption.
    /// </summary>
    /// <typeparam name="T">Any object that derives from MarshalByRefObject.</typeparam>
    public sealed class MarshalByRefList<T> : MarshalByRefObject, IEnumerable, IList<T>
        where T : MarshalByRefObject
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public MarshalByRefList()
        {
            _List = new ArrayList();
        }

        #endregion

        #region Declarations
        #region _Members_
        private ArrayList _List;

        #endregion
        #endregion

        #region Methods
        #region _Public_
        /// <summary>
        /// Adds an object to the list.
        /// </summary>
        /// <param name="item">A T that is the item to add.</param>
        /// <returns>An int indicating the index of the added item.</returns>
        public int Add(T item)
        {
            return _List.Add(item);
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
        /// <returns>A bool indicating the result.</returns>
        public bool Contains(T item)
        {
            return _List.Contains(item);
        }

        /// <summary>
        /// Removes an item from the list.
        /// </summary>
        /// <param name="item">A T that is the item to remove.</param>
        public void Remove(T item)
        {
            _List.Remove(item);
        }

        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the item at the specified index in the list.
        /// </summary>
        /// <param name="index">An int that is the index of the item in the list.</param>
        /// <returns>A T that is the item</returns>
        public T this[int index]
        {
            get
            {
                T ReturnValue = default(T);
                object TheItem;

                TheItem = _List[index];
                if (TheItem != null && TheItem is T)
                    ReturnValue = (T)TheItem;

                return ReturnValue;
            }
            set
            {
                _List[index] = value;
            }
        }

        /// <summary>
        /// Gets the count of items in the list.
        /// </summary>
        public int Count
        {
            get { return _List.Count; }
        }

        #endregion

        #region Interfaces
        #region _ICollection<T>_
        void ICollection<T>.Add(T item)
        {
            _List.Add(item);
        }

        void ICollection<T>.Clear()
        {
            _List.Clear();
        }

        bool ICollection<T>.Contains(T item)
        {
            return _List.Contains(item);
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            _List.CopyTo(array, arrayIndex);
        }

        int ICollection<T>.Count
        {
            get { return _List.Count; }
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return _List.IsReadOnly; }
        }

        bool ICollection<T>.Remove(T item)
        {
            _List.Remove(item);
            return true;
        }

        #endregion
        #region _IEnumerable_
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        #endregion
        #region IEnumerable<T> Members
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)_List.GetEnumerator();
        }

        #endregion
        #region _IList<T>_
        
        int IList<T>.IndexOf(T item)
        {
            return _List.IndexOf(item);
        }

        void IList<T>.Insert(int index, T item)
        {
            _List.Insert(index, item);
        }

        void IList<T>.RemoveAt(int index)
        {
            _List.RemoveAt(index);
        }

        T IList<T>.this[int index]
        {
            get { return (T)_List[index]; }
            set { _List[index] = value; }
        }

        #endregion
        #endregion
    }
}