using System;
using System.Linq;
using System.Threading.Tasks;

namespace SDL.Server.Services
{
    public class CalcService : ICalcService
    {
        public Task<int> SumAsync(int[] numbers)
        {
            return Task.FromResult(numbers.Sum());
        }
    }
}
