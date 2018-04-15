using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TelloSharp
{
    internal class TelloUpdClient : ITelloUpdClient
    {
        private readonly static IPAddress telloAddress = IPAddress.Parse("192.168.10.1");
        private readonly static int telloPort = 8889;
        private UdpClient _udpClient;
        private IPEndPoint _remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        private IPEndPoint telloEndPoint = new IPEndPoint(telloAddress, telloPort);

        internal TelloUpdClient()
        {
            _udpClient = new UdpClient();
            _udpClient.Client.ReceiveTimeout = 3000;
        }
        
        public async Task<bool> SendActionAsync(string value)
        {
            var actionResult = await SendAsync(value);
            if (actionResult == "OK")
                return true;

            if (actionResult == "FALSE")
                return false;

            throw new Exception("Unknown Result", new Exception(actionResult));
        }

        public async Task<string> SendAsync(string value)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes(value?.ToLower());
            if (!_udpClient.Client.Connected)
            {
                _udpClient.Connect(telloEndPoint);
                await SendActionAsync("command");
            }
            await _udpClient.SendAsync(buffer, buffer.Length);

            var response = _udpClient.Receive(ref _remoteIpEndPoint);
            return System.Text.Encoding.UTF8.GetString(response);
        }

        public void Dispose()
        {
            if (_udpClient.Client.Connected)
            {
                _udpClient.Close();
            }
            _udpClient.Dispose();
        }
    }
}