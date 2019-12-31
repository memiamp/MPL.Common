using System;

namespace MPL.Common
{
    /// <summary>
    /// A class that defines extension methods to System.Array.
    /// </summary>
    public static class ArrayExtensions
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Appends an item to the end of the array.
        /// </summary>
        /// <typeparam name="T">The type of the array items.</typeparam>
        /// <param name="array">An array of T that is the array to append to.</param>
        /// <param name="item">A T that is the item to append to the array.</param>
        /// <returns>An array of T that has had an item appended to it.</returns>
        public static T[] Append<T>(this T[] array, T item)
        {
            if (array == null)
                throw new ArgumentException("The specified array is NULL", nameof(array));

            return InsertAt(array, array.Length, item);
        }

        /// <summary>
        /// Inserts an item into an array at the specified position.
        /// </summary>
        /// <typeparam name="T">The type of the array items.</typeparam>
        /// <param name="array">An array of T that is the array to insert into.</param>
        /// <param name="position">An int indicating the position to insert into.</param>
        /// <param name="item">A T that is the item to insert into the array.</param>
        /// <returns>An array of T that has had an item inserted into it.</returns>
        public static T[] InsertAt<T>(this T[] array, int position, T item)
        {
            T[] returnValue;

            if (array == null)
                throw new ArgumentException("The specified array is NULL", nameof(array));
            if (position < 0 || position > array.Length)
                throw new ArgumentException("The specified position is invalid", nameof(position));

            returnValue = new T[array.Length + 1];
            if (position > 0)
                Array.Copy(array, 0, returnValue, 0, position);
            returnValue[position] = item;
            if (position < array.Length)
                Array.Copy(array, position, returnValue, position + 1, array.Length - position);

            return returnValue;
        }
        /// <summary>
        /// Inserts items into an array at the specified position.
        /// </summary>
        /// <typeparam name="T">The type of the array items.</typeparam>
        /// <param name="array">An array of T that is the array to insert into.</param>
        /// <param name="position">An int indicating the position to insert into.</param>
        /// <param name="items">An array of T that is the items to insert into the array.</param>
        /// <returns>An array of T that has had an item inserted into it.</returns>
        public static T[] InsertAt<T>(this T[] array, int position, T[] items)
        {
            T[] returnValue;

            if (array == null)
                throw new ArgumentException("The specified array is NULL", nameof(array));
            if (position < 0 || position > array.Length)
                throw new ArgumentException("The specified position is invalid", nameof(position));

            returnValue = new T[array.Length + items.Length];
            if (position > 0)
                Array.Copy(array, 0, returnValue, 0, position);
            for (int i = 0; i < items.Length; i++)
                returnValue[position + i] = items[i];
            if (position < array.Length)
                Array.Copy(array, position, returnValue, position + items.Length, array.Length - position);

            return returnValue;
        }

        #endregion
        #endregion

    }
}