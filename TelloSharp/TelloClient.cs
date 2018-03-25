using System;
using System.Net;
using System.Threading.Tasks;

namespace TelloSharp
{
    public class TelloClient: ITelloDevice
    {
        private readonly IPAddress telloAddress = IPAddress.Parse("192.168.10.1");
        private readonly int telloPort = 8889;

        public Task<bool> TakeOff()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Land()
        {
            throw new NotImplementedException();
        }
        
        public async Task<bool> Fly(FlyDirection flyDirection, int distance)
        {
            if (distance < 20 || distance > 500)
                throw new ArgumentOutOfRangeException(nameof(distance), "Distance should be between 20 and 500");


            throw new NotImplementedException();
        }

        public async Task<bool> Rotate(RotateDirection rotateDirection, int degree)
        {
            if (degree < 1 || degree > 3600)
                throw new ArgumentOutOfRangeException(nameof(degree), "Degree should be between 1 and 3600");

            throw new NotImplementedException();
        }

        public async Task<bool> Flip(FlipDirection flipDirection)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SetSpeed(int speed)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetBatteryLevel()
        {
            throw new NotImplementedException();
        }

        public async Task<TimeSpan> GetFlightTime()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetSpeed()
        {
            throw new NotImplementedException();
        }
    }
}
