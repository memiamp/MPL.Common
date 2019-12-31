using System;
using System.Collections.Generic;

namespace MPL.Common.Collections.Concurrent
{
    /// <summary>
    /// An interface that defines a synchronised single-instance list of the specified type.
    /// </summary>
    /// <typeparam name="T">A T that is the type that is contained by the list.</typeparam>
    /// <remarks>Implementors must ensure that the list is synchronised.</remarks>
    public interface IConcurrentList<T> : IList<T>
    { }
}