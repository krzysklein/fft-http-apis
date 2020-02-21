using GraphQL.Server.Models;
using GraphQL.Types;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQL.Server.Graphs
{
    public class WeatherForecastGraphMapping : ObjectGraphType<WeatherForecast>
    {
        public WeatherForecastGraphMapping()
        {
            Name = "WeatherForecast";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the Forecast.");
            Field(x => x.Date).Description("The Date of the Forecast");
            Field(x => x.TemperatureC).Description("Temperature in Celsius");
            Field(x => x.TemperatureF).Description("Temperature in Fahrenheit");
            Field(x => x.Summary).Description("Summary");
        }
    }

    public class GraphQLQuery
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }

    public class QueryGraphMapping : ObjectGraphType
    {
        private readonly List<WeatherForecast> weatherForecasts;

        public QueryGraphMapping(List<WeatherForecast> weatherForecasts)
        {
            this.weatherForecasts = weatherForecasts;

            Field<ListGraphType<WeatherForecastGraphMapping>>(
                "WeatherForecasts",
                resolve: context =>
                {
                    return weatherForecasts;
                });
        }
    }
}
