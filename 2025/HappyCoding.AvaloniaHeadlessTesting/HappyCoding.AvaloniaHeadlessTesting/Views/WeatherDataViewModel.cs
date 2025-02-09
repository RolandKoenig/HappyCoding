using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HappyCoding.AvaloniaHeadlessTesting.Views;

public partial class WeatherDataViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsFetching))]
    [NotifyPropertyChangedFor(nameof(IsDataLoaded))]
    private WeatherForecast[]? _forecasts;
    
    public bool IsFetching => this.Forecasts is null;
    
    public bool IsDataLoaded => this.Forecasts is not null;
    
    public WeatherDataViewModel()
    {
        SimulateFetchWeatherData();
    }

    private async void SimulateFetchWeatherData()
    {
        try
        {
            await Task.Delay(1000);
            
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            this.Forecasts = Enumerable.Range(1, 100).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            }).ToArray();
        }
        catch (Exception e)
        {
            this.Forecasts = [];
        }
    }
    
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }
        
        public int TemperatureC { get; set; }
        
        public string? Summary { get; set; }
        
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}