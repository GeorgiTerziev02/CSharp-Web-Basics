namespace SulsApp
{
    using SIS.MvcFramework;
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            await WebHost.StartAsync(new StartUp());
        }
    }
}
