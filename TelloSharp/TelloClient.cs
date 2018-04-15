using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading.Tasks;

namespace TelloSharp
{
    public class TelloClient : ITelloDevice
    {
        private ITelloUpdClient _telloUpdClient;

        public object _udpClient { get; private set; }

        public TelloClient():
            this(new TelloUpdClient())
        {
        }

        internal TelloClient(ITelloUpdClient telloUpdClient)
        {
            _telloUpdClient = telloUpdClient;
        }
        
        public async Task<bool> TakeOff()
        {
            return await _telloUpdClient.SendActionAsync("takeoff");
        }

        public async Task<bool> Land()
        {
            return await _telloUpdClient.SendActionAsync("land");
        }

        public async Task<bool> Fly(FlyDirection flyDirection, int distance)
        {
            if (distance < 20 || distance > 500)
                throw new ArgumentOutOfRangeException(nameof(distance), "Distance should be between 20 and 500");

            return await _telloUpdClient.SendActionAsync($"{GetCommand(flyDirection)} {distance}");
        }
       
        public async Task<bool> Rotate(RotateDirection rotateDirection, int degree)
        {
            if (degree < 1 || degree > 3600)
                throw new ArgumentOutOfRangeException(nameof(degree), "Degree should be between 1 and 3600");

            return await _telloUpdClient.SendActionAsync($"{GetCommand(rotateDirection)} {degree}");
        }

        public async Task<bool> Flip(FlipDirection flipDirection)
        {
            return await _telloUpdClient.SendActionAsync(GetCommand(flipDirection));
        }

        public async Task<bool> SetSpeed(int speed)
        {
            return await _telloUpdClient.SendActionAsync($"speed {speed}");
        }

        public async Task<int> GetBatteryLevel()
        {
            var result = await _telloUpdClient.SendAsync("battery?");
            int batteryLevel = 0;

            if (!int.TryParse(result, out batteryLevel))
                throw new Exception("Unable to get battery level");

            return batteryLevel;
        }

        public async Task<TimeSpan> GetFlightTime()
        {
            var flightTime = await _telloUpdClient.SendAsync("time?");
            flightTime = flightTime?.TrimEnd('s');
            int flightTimeSec = 0;

            if (!int.TryParse(flightTime, out flightTimeSec))
                throw new Exception("Unable to get flight time");
            
            return TimeSpan.FromSeconds(flightTimeSec);
        }

        public async Task<double> GetSpeed()
        {
            var result = await _telloUpdClient.SendAsync("speed?");
            double speed = 0;

            if (!double.TryParse(result, out speed))
                throw new Exception("Unable to get speed");

            return speed;
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
            _telloUpdClient?.Dispose();
        }
    }
}
