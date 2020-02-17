using SIS.MvcFramework;
using System;
using System.Threading.Tasks;

namespace SharedTrip
{
    public static class Program
    {
        public static async Task Main()
        {
            //the front seats arent counted as free seats => seats = seats - 2

            await WebHost.StartAsync(new Startup());
        }
    }
}
