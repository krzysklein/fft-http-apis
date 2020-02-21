using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDL.Client.Services
{
	public interface ICalcService
    {
        Task<int> SumAsync(int[] numbers);
    }
}
