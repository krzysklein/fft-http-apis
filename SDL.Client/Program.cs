using SDL.Client.Services;
using System;
using System.Threading.Tasks;

namespace SDL.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ICalcService calcService = new CalcServiceClient("https://localhost:5001");
            var sum = await calcService.SumAsync(new int[] { 1, 2, 3, 4 });
            Console.WriteLine($"Sum = {sum}");
        }
    }
}
