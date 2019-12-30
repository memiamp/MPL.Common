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

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new ConcurrentEnumerator<T>(_List.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        int IList<T>.IndexOf(T item)
        {
            return _List.IndexOf(item);
        }

        void IList<T>.Insert(int index, T item)
        {
            _List.Insert(index, item);
        }

        bool ICollection<T>.Remove(T item)
        {
            bool ReturnValue = false;

            if (_List.Contains(item))
            {
                _List.Remove(item);
                ReturnValue = true;
            }

            return ReturnValue;
        }

        void IList<T>.RemoveAt(int index)
        {
            _List.RemoveAt(index);
        }

        #endregion
        #region __Properties__
        T IList<T>.this[int index]
        {
            get { return (T)_List[index]; }
            set { _List[index] = value; }
        }

        int ICollection<T>.Count
        {
            get { return _List.Count; }
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return _List.IsReadOnly; }
        }

        #endregion
        #endregion
        #endregion
    }
}