using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using REST.Server.Models;

namespace REST.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly List<WeatherForecast> weatherForecasts;
        private static int nextId;

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
            nextId = weatherForecasts.Count + 1;
        }

        // GET: api/WeatherForecast
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<WeatherForecast>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            return Ok(weatherForecasts);
        }

        // GET: api/WeatherForecast/{id}
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(typeof(WeatherForecast), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<WeatherForecast> Get(int id)
        {
            var weatherForecast = weatherForecasts.SingleOrDefault(t => t.Id == id);
            if (weatherForecast != null)
                return Ok(weatherForecast);
            else
                return NotFound();
        }

        // POST: api/WeatherForecast
        [HttpPost]
        [ProducesResponseType(typeof(WeatherForecast), StatusCodes.Status201Created)]
        public ActionResult<WeatherForecast> Post([FromBody] WeatherForecast weatherForecast)
        {
            weatherForecast.Id = nextId++;
            weatherForecasts.Add(weatherForecast);
            return Created($"api/WeatherForecast/{weatherForecast.Id}", weatherForecast);
        }

        // PUT: api/WeatherForecast/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Put(int id, [FromBody] WeatherForecast weatherForecast)
        {
            weatherForecasts.RemoveAll(t => t.Id == id);
            weatherForecasts.Add(weatherForecast);
            return NoContent();
        }

        // DELETE: api/WeatherForecast/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Delete(int id)
        {
            weatherForecasts.RemoveAll(t => t.Id == id);
            return NoContent();
        }
    }
}
