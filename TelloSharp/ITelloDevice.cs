using System;
using System.Threading.Tasks;

namespace TelloSharp
{
    public interface ITelloDevice: IDisposable
    {
        /// <summary>
        /// Auto takeoff
        /// </summary>
        /// <returns></returns>
        Task<bool> TakeOff();

        /// <summary>
        /// Auto landing
        /// </summary>
        /// <returns></returns>
        Task<bool> Land();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flyDirection"></param>
        /// <param name="distance">20-500 cm</param>
        /// <returns></returns>
        Task<bool> Fly(FlyDirection flyDirection, int distance);

        /// <summary>
        /// Rotate x°
        /// </summary>
        /// <param name="rotateDirection"></param>
        /// <param name="degree">1-3600°</param>
        /// <returns></returns>
        Task<bool> Rotate(RotateDirection rotateDirection, int degree);

        Task<bool> Flip(FlipDirection flipDirection);

        /// <summary>
        /// Set current speed
        /// </summary>
        /// <param name="speed">1-100 cm/s</param>
        /// <returns></returns>
        Task<bool> SetSpeed(int speed);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<double> GetSpeed();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<int> GetBatteryLevel();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<TimeSpan> GetFlightTime();
    }
}