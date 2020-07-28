using System;
using System.Runtime.Serialization;

namespace MPL.Common
{
    /// <summary>
    /// A class that defines base functionality of a comparable object.
    /// </summary>
    /// <typeparam name="T">A T that is the type to be compared.</typeparam>
    [DataContract()]
    public abstract class ComparableBase<T> : IComparable, IComparable<T>
    {
        #region Methods
        #region _Protected_
        /// <summary>
        /// Overridden in derived classes to compare this object with the specified other object.
        /// </summary>
        /// <param name="other">A T that is the other class to compare this object to.</param>
        /// <returns>An int indicating the result of the comparison.</returns>
        protected abstract int OnCompareTo(T other);

        #endregion
        #endregion

        #region Interfaces
        #region _IComparable_
        int IComparable.CompareTo(object obj)
        {
            if (obj != null)
            {
                if (obj is T other)
                    return ((IComparable<T>)this).CompareTo(other);
                else
                    throw new ArgumentException("The specified object is not valid for a comparison with this object", nameof(obj));
            }
            else
                throw new ArgumentException("A comparison with a NULL object is not permitted", nameof(obj));
        }

        #endregion
        #region _IComparable<T>_
        int IComparable<T>.CompareTo(T other)
        {
            if (other == null) throw new ArgumentException("A comparison with a NULL object is not permitted", nameof(other));

            return OnCompareTo(other);
        }

        #endregion
        #endregion
    }
}
