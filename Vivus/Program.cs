using System;
using System.Threading.Tasks;

namespace Vivus
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var monitor = new Monitor(@"node", "./test.js");

            monitor.Run();
            
            while (true)
            {
                Console.WriteLine($"monitor output: {monitor.StandardOutput}");
                await Task.Delay(1000);
            }
        }
    }
}