using System;
using System.Collections.Generic;
using System.Linq;

namespace HappyCoding.AvaloniaAppZoom.Data;

public class WeatherForecast
{
    public DateOnly Date { get; set; }
        
    public int TemperatureC { get; set; }
        
    public string? Summary { get; set; }
        
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public static IEnumerable<WeatherForecast> GenerateForecasts(int count)
    {
        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        return Enumerable.Range(1, count).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        }).ToArray();
    }
}