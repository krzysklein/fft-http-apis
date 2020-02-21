using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SDL.Server.Services;

namespace SDL.Server.Controllers
{
    [ApiController]
    [Route("api/CalcService")]
    public class CalcServiceController : ControllerBase
    {
        private readonly ICalcService CalcService;
        public CalcServiceController(ICalcService CalcService)
        {
            this.CalcService = CalcService;
        }
        [HttpPost("Sum")]
        public async Task<ActionResult<int>> SumAsync([FromBody] CalcService_Sum_RequestDto request)
        {
            var result = await this.CalcService.SumAsync(request.numbers);
            return Ok(result);
        }

        public class CalcService_Sum_RequestDto
        {
            public int[] numbers { get; set; }
        }
    }
}
