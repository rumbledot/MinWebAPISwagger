using WebSwagger.Data;
using WebSwagger.Endpoints;
using WebSwagger.Helpers;
using WebSwagger.Models;
using WebSwagger.Services;
using WebSwagger.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureServices();

var app = builder.BuildApp();

app.Services.GetService(typeof(WeatherData));
app.Services.GetService(typeof(CustomerData));

app.ConfigureApp();

app.UseHttpsRedirection();

app.SetCustomerEndpoint();

app.SetWeatherEndpoint();

app.SetSimonsaysEndpoint();

app.Run();