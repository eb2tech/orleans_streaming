﻿namespace orleans_streaming.ApiService;

internal static class EndpointExtensions
{
    internal static IEndpointRouteBuilder AddWeatherEndpoint(this IEndpointRouteBuilder endpoints)
    {
        string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];
        endpoints.MapGet("/weatherforecast", () =>
           {
               var forecast = Enumerable.Range(1, 5).Select(index =>
                                                                new WeatherForecast
                                                                (
                                                                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                                                                    Random.Shared.Next(-20, 55),
                                                                    summaries[Random.Shared.Next(summaries.Length)]
                                                                ))
                                        .ToArray();
               return forecast;
           })
           .WithName("GetWeatherForecast");

        return endpoints;
    }

    internal static IEndpointRouteBuilder AddDefaultEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/", () => "Hello there!");
        return endpoints;
    }
}

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary);
