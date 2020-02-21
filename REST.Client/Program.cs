using Refit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace REST.Client
{
    public interface IWeatherForecastApi
    {
        [Get("/api/WeatherForecast")]
        Task<List<WeatherForecastDto>> GetWeatherForecasts();

        [Get("/api/WeatherForecast/{id}")]
        Task<WeatherForecastDto> GetWeatherForecast(int id);

        [Post("/api/WeatherForecast")]
        Task<WeatherForecastDto> CreateWeatherForecast(WeatherForecastDto weatherForecastDto);

        [Put("/api/WeatherForecast/{id}")]
        Task UpdateWeatherForecast(int id, WeatherForecastDto weatherForecastDto);

        [Delete("/api/WeatherForecast/{id}")]
        Task DeleteWeatherForecast(int id);
    }

    [DebuggerDisplay("({Id}) {Date}: {TemperatureC}C, {Summary}")]
    public class WeatherForecastDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public int TemperatureF { get; set; }
        public string Summary { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            var weatherForecastApi = RestService.For<IWeatherForecastApi>("https://localhost:44346/");
            
            var weatherForecasts = await weatherForecastApi.GetWeatherForecasts();
            
            var weatherForecast = await weatherForecastApi.GetWeatherForecast(2);
            
            var newWeatherForecast = await weatherForecastApi.CreateWeatherForecast(weatherForecast);

            newWeatherForecast.Summary = "test";
            await weatherForecastApi.UpdateWeatherForecast(newWeatherForecast.Id, newWeatherForecast);
            weatherForecasts = await weatherForecastApi.GetWeatherForecasts();

            await weatherForecastApi.DeleteWeatherForecast(newWeatherForecast.Id);
            weatherForecasts = await weatherForecastApi.GetWeatherForecasts();
        }
    }
}
