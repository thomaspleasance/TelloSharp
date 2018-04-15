using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading.Tasks;

namespace TelloSharp
{
    public class TelloClient : ITelloDevice
    {
        private IPEndPoint _remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        private readonly static IPAddress telloAddress = IPAddress.Parse("192.168.10.1");
        private readonly static int telloPort = 8889;
        private IPEndPoint telloEndPoint = new IPEndPoint(telloAddress, telloPort);
        private UdpClient _udpClient;

        public TelloClient()
        {
            _udpClient = new UdpClient();
            _udpClient.Client.ReceiveTimeout = 3000;
        }

        public async Task<bool> TakeOff()
        {
            return await SendActionAsync("takeoff");
        }

        public async Task<bool> Land()
        {
            return await SendActionAsync("land");
        }

        public async Task<bool> Fly(FlyDirection flyDirection, int distance)
        {
            if (distance < 20 || distance > 500)
                throw new ArgumentOutOfRangeException(nameof(distance), "Distance should be between 20 and 500");

            return await SendActionAsync($"{GetCommand(flyDirection)} {distance}");
        }
       
        public async Task<bool> Rotate(RotateDirection rotateDirection, int degree)
        {
            if (degree < 1 || degree > 3600)
                throw new ArgumentOutOfRangeException(nameof(degree), "Degree should be between 1 and 3600");

            return await SendActionAsync($"{GetCommand(rotateDirection)} {degree}");
        }

        public async Task<bool> Flip(FlipDirection flipDirection)
        {
            return await SendActionAsync(GetCommand(flipDirection));
        }

        public async Task<bool> SetSpeed(int speed)
        {
            return await SendActionAsync($"speed {speed}");
        }

        public async Task<int> GetBatteryLevel()
        {
            var result = await SendAsync("battery?");
            int batteryLevel = 0;

            if (!int.TryParse(result, out batteryLevel))
            {
                throw new Exception("Unable to get battery level");
            }
            return batteryLevel;
        }

        public async Task<TimeSpan> GetFlightTime()
        {
            var flightTime = await SendAsync("time?");
            Console.WriteLine(flightTime);
            return new TimeSpan();
        }

        public async Task<double> GetSpeed()
        {
            var result = await SendAsync("speed?");
            double speed = 0;

            if (!double.TryParse(result, out speed))
            {
                throw new Exception("Unable to get speed");
            }
            return speed;
        }

        private async Task<bool> SendActionAsync(string value)
        {
            var actionResult = await SendAsync(value);
            if (actionResult == "OK")
                return true;

            if (actionResult == "FALSE")
                return false;

            throw new Exception("Unknown Result", new Exception(actionResult));
        }

        private async Task<string> SendAsync(string value)
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

        private string GetCommand<T>(T enumValue)
        {
            var type = typeof(T);
            var memInfo = type.GetMember(type.GetEnumName(enumValue));
            var telloCommand = memInfo.Length > 0 ? memInfo[0].GetCustomAttribute<TelloCommand>() : null;
            return (telloCommand?.Command ?? type.GetEnumName(enumValue))?.ToLower();
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
