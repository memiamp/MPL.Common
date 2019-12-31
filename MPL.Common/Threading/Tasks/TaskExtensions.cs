using System;
using System.Threading;
using System.Threading.Tasks;

namespace MPL.Common.Threading.Tasks
{
    /// <summary>
    /// A class that defines extensions to the Task object.
    /// </summary>
    public static class TaskExtensions
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Allows notification of the cancellation timeout of a non-cancelable async operation.
        /// </summary>
        /// <typeparam name="T">A T that is the type of the result produced by this Task.</typeparam>
        /// <param name="task">A Task of type T is the task to provide timeout notification for.</param>
        /// <param name="cancellationToken">A CancellationToken that is the cancellation token to use.</param>
        /// <returns>A Task of type T that is the associated task.</returns>
        public static async Task<T> WithWaitCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
        {
            TaskCompletionSource<bool> completionSource;

            completionSource = new TaskCompletionSource<bool>();
            using (cancellationToken.Register(t => ((TaskCompletionSource<bool>)t).TrySetResult(true), completionSource))
            {
                if (task != await Task.WhenAny(task, completionSource.Task))
                    throw new OperationCanceledException(cancellationToken);
            }

            return await task;
        }

        #endregion
        #endregion
    }
}