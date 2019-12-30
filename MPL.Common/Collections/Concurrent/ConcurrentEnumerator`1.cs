using System;
using System.Collections;
using System.Collections.Generic;

namespace MPL.Common.Collections.Concurrent
{
    /// <summary>
    /// A class that defines an enumerator for a concurrent collection.
    /// </summary>
    /// <typeparam name="T">A T that is the type being enumerated.</typeparam>
    public class ConcurrentEnumerator<T> : IEnumerator<T>
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the class with the specified parameters.
        /// </summary>
        /// <param name="enumerator">An IEnumerator that is the enumerator.</param>
        internal ConcurrentEnumerator(IEnumerator enumerator)
        {
            _Enumerator = enumerator;
        }

        #endregion

        #region Declarations
        #region _Members_
        private readonly IEnumerator _Enumerator;

        #endregion
        #endregion

        #region Interfaces
        #region _IEnumerator<T>_
        #region __Methods__
        void IDisposable.Dispose()
        { }

        bool IEnumerator.MoveNext()
        {
            return _Enumerator.MoveNext();
        }

        void IEnumerator.Reset()
        {
            _Enumerator.Reset();
        }

        #endregion
        #region __Properties__
        object IEnumerator.Current
        {
            get { return (T)_Enumerator.Current; }
        }

        T IEnumerator<T>.Current
        {
            get { return (T)_Enumerator.Current; }
        }

        #endregion
        #endregion
        #endregion
    }
}