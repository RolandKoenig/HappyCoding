using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using HappyCoding.AvaloniaAppZoom.Data;

namespace HappyCoding.AvaloniaAppZoom;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ScaleX), nameof(ScaleY))]
    private int _selectedZoomLevel = 100;
    
    public int[] SupportedZoomLevels { get; } = [50, 75, 100, 125, 150, 200, 250];

    public double ScaleX => this.SelectedZoomLevel / 100.0;
    
    public double ScaleY => this.SelectedZoomLevel / 100.0;

    public IReadOnlyCollection<WeatherForecast> Forecasts { get; }
        = WeatherForecast.GenerateForecasts(100).ToArray();
}