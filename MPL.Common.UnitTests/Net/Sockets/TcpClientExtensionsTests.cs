using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPL.Common.TestHelpers;
using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace MPL.Common.Net.Sockets
{
    [TestClass]
    public class TcpClientExtensionsTests
    {
        [TestMethod]
        public void ExtensionMethod_GetState_Exists()
        {
            Assert.IsTrue(ExtensionMethodTestHelper.ContainsNamedExtensionMethod(typeof(TcpClient), typeof(TcpClientExtensions), nameof(TcpClientExtensions.GetState)));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times")]
        [TestMethod]
        public void GetState_ClosedClient_ReturnsCorrectStatus()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();

            using (TcpClient client = new TcpClient())
            {
                client.Connect("localhost", ((IPEndPoint)listener.LocalEndpoint).Port);
                client.Close();
                Assert.AreEqual(TcpState.Unknown, client.GetState());
            }


            listener.Stop();
        }

        [TestMethod]
        public void GetState_ClosedListener_ReturnsCorrectStatus()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();

            using (TcpClient client = new TcpClient())
            {
                client.Connect("localhost", ((IPEndPoint)listener.LocalEndpoint).Port);
                Assert.AreEqual(TcpState.Established, client.GetState());
                listener.Stop();
                Assert.AreEqual(TcpState.Unknown, client.GetState());
            }
        }

        [TestMethod]
        public void GetState_EstablishedClient_ReturnsCorrectStatus()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();

            using (TcpClient client = new TcpClient())
            {
                client.Connect("localhost", ((IPEndPoint)listener.LocalEndpoint).Port);
                Assert.AreEqual(TcpState.Established, client.GetState());
            }

            listener.Stop();
        }

        [TestMethod]
        public void GetState_NewTcpClient_ReturnsCorrectStatus()
        {
            using (TcpClient client = new TcpClient())
            {
                Assert.AreEqual(TcpState.Unknown, client.GetState());
            }
        }
    }
}