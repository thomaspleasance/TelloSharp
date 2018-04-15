using System;
using System.Threading;
using System.Threading.Tasks;

namespace TelloSharp.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            Task.Run(async () =>
           {
               using (TelloClient telloClient = new TelloClient())
               {
                   try
                   {
                       await telloClient.Fly(FlyDirection.Up, 40);

                       await telloClient.TakeOff();
                       Thread.Sleep(1000);

                       await telloClient.Fly(FlyDirection.Up, 40);

                       for (int i = 0; i < 100; i++)
                       {

                           var t = await telloClient.GetFlightTime();
                           var b = await telloClient.GetBatteryLevel();

                           var s = await telloClient.GetSpeed();

                           Console.WriteLine("Time {0}, Battery {1}, Speed {2}", t, b, s);

                           Thread.Sleep(1000);

                       }
                       await telloClient.Land();
                   }
                   catch (Exception e)
                   {

                       throw;
                   }
               }
           });

            Console.ReadKey();
        }
    }
}
