using WebSwagger.Models;

namespace WebSwagger.Data
{
    public class WeatherData
    {
        private static string[] summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecast[] GetWeatherForecasts()
        {
            return Enumerable.Range(1, 7).Select(index =>
                    new WeatherForecast
                    (
                        DateTime.Now.AddDays(index),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
                    ))
                    .ToArray();
        }

        public WeatherForecast GetWeatherForecast()
        {
            return new WeatherForecast (
                        DateTime.Now.AddDays(1),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)] 
                        );
        }
    }
}
