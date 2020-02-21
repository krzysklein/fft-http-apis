using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDL.Server.Services
{
    public interface ICalcService
    {
        Task<int> SumAsync(int[] numbers);
    }
}
