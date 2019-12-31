using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MPL.Common.Net.Sockets
{
    /// <summary>
    /// A class that defines extensions to a Network Stream.
    /// </summary>
    public static class NetworkStreamExtensions
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Asynchronously reads all available data.
        /// </summary>
        /// <param name="stream">A NetworkStream that is the stream to read from.</param>
        /// <returns>A Task of array of byte associated with the operation.</returns>
        public static async Task<byte[]> ReadAvailable(this NetworkStream stream)
        {
            byte[] buffer = new byte[500];
            int bytesRead = 0;
            MemoryStream data;
            byte[] returnValue = null;

            data = new MemoryStream();
            while (stream.DataAvailable && (bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false)) > 0)
                data.Write(buffer, 0, bytesRead);
            if (data.Length > 0)
                returnValue = data.ToArray();

            return returnValue;
        }

        #endregion
        #endregion
    }
}