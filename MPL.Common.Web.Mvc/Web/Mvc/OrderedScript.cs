using System;
using System.Web.WebPages;

namespace MPL.Common.Web.Mvc
{
    /// <summary>
    /// A class that defines a ordered script.
    /// </summary>
    internal sealed class OrderedScript : IComparable, IComparable<OrderedScript>
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="order">An int indicating the order of the script in the loading list.</param>
        /// <param name="script">A function delagate that accepts object and returns HelperResult that contains the script.</param>
        internal OrderedScript(int order, Func<object, HelperResult> script)
        {
            Order = order;
            Script = script;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the order of the script in the loading list.
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Gets or sets the script.
        /// </summary>
        public Func<object, HelperResult> Script { get; set; }

        #endregion

        #region Interfaces
        #region _IComparable_
        int IComparable.CompareTo(object obj)
        {
            int ReturnValue = 0;

            if (obj != null)
            {
                if (obj is OrderedScript)
                    ReturnValue = ((IComparable<OrderedScript>)this).CompareTo((OrderedScript)obj);
                else
                    throw new ArgumentException("Cannot compare a OrderedScript with an object of a different type", "obj");
            }
            else
                throw new ArgumentException("The specified comparison object is NULL", "obj");

            return ReturnValue;
        }

        #endregion
        #region _IComparable<OrderedScript>_
        int IComparable<OrderedScript>.CompareTo(OrderedScript other)
        {
            int ReturnValue = 0;

            if (other != null)
                ReturnValue = Order.CompareTo(other.Order);
            else
                throw new ArgumentException("The specified comparison object is NULL", "other");

            return ReturnValue;
        }

        #endregion
        #endregion
    }
}