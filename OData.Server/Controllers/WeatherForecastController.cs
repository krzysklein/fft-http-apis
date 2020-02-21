using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OData.Server.Models;

namespace OData.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ODataController
    {
        private static readonly List<WeatherForecast> weatherForecasts;

        static WeatherForecastController()
        {
            string[] Summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            var rng = new Random();
            weatherForecasts = Enumerable.Range(1, 5)
                .Select(index => new WeatherForecast
                {
                    Id = index,
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToList();
        }

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [EnableQuery()]
        public IQueryable<WeatherForecast> Get(string qwerty)
        {
            return weatherForecasts.AsQueryable();
        }
    }
}
