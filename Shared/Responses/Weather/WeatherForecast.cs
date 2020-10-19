using System;
namespace Shared.Responses.Weather
{
    public class WeatherForecast
    {
        public WeatherForecast()
        {
        }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF { get; set; }
    }
}
