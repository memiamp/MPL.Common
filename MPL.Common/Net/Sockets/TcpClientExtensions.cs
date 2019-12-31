using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace MPL.Common.Net.Sockets
{
    /// <summary>
    /// A class that defines extensions to a Tcp Client.
    /// </summary>
    public static class TcpClientExtensions
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Gets the state of the specified TCP client.
        /// </summary>
        /// <param name="tcpClient">A TcpClient that is the client to get the state of.</param>
        /// <returns>A TcpState indicating the state of the client.</returns>
        public static TcpState GetState(this TcpClient tcpClient)
        {
            TcpConnectionInformation connectionInformation;
            TcpState returnValue = TcpState.Unknown;

            if (tcpClient.Client != null)
            {
                connectionInformation = IPGlobalProperties.GetIPGlobalProperties()
                                                          .GetActiveTcpConnections()
                                                          .SingleOrDefault(x => x.LocalEndPoint.Equals(tcpClient.Client.LocalEndPoint) &&
                                                                                x.RemoteEndPoint.Equals(tcpClient.Client.RemoteEndPoint));
                if (connectionInformation != null)
                    returnValue = connectionInformation.State;
            }

            return returnValue;
        }

        #endregion
        #endregion
    }
}