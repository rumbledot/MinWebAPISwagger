using Microsoft.AspNetCore.Mvc;
using WebSwagger.Data;
using WebSwagger.Models;
using WebSwagger.Services;

namespace WebSwagger.Endpoints
{
    public static class WeatherEndpoint
    {
        private static string[] summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };        

        public static WebApplication SetWeatherEndpoint(this WebApplication app)
        {
            app.MapGet("/weatherforecast",
                [ProducesResponseType(200, Type= typeof(WeatherForecast))]
            (WeatherData data) =>
            {
                return Results.Ok(data.GetWeatherForecasts());
            })
                .Produces<WeatherForecast>(StatusCodes.Status200OK)
                .AllowAnonymous()
                .WithName("GetWeatherForecasts")
                .WithTags("Weather");

            app.MapGet("/today",
                [ProducesResponseType(200, Type = typeof(WeatherForecast))]
            (WeatherData data) => 
            { 
                return data.GetWeatherForecast();
            })
                .Produces<WeatherForecast>(StatusCodes.Status200OK)
                .AllowAnonymous()
                .WithName("GetTodayWeatherForecasts")
                .WithTags("Weather");

            return app;
        }
    }
}
