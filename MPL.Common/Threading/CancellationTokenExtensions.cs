using System;
using System.Threading;
using System.Threading.Tasks;

namespace MPL.Common.Threading
{
    /// <summary>
    /// A class that defines extensions to a CancellationToken.
    /// </summary>
    public static class CancellationTokenExtensions
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Obtains a Task associated with the specified cancellation token.
        /// </summary>
        /// <param name="cancellationToken">A CancellationToken that is the cancellation token.</param>
        /// <returns>A Task associated with the specified cancellation token.</returns>
        public static Task AsTask(this CancellationToken cancellationToken)
        {
            TaskCompletionSource<object> taskCompletionSource;
            Task returnValue;

            taskCompletionSource = new TaskCompletionSource<object>();
            cancellationToken.Register(() => taskCompletionSource.SetResult(new object()));
            returnValue = taskCompletionSource.Task;

            return returnValue;
        }

        #endregion
        #endregion
    }
}