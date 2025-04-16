using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HappyCoding.TemperatureViewer.Model;
using HappyCoding.TemperatureViewer.Services;

namespace HappyCoding.TemperatureViewer;

public partial class TemperatureViewerViewModel(IMeasurementService measurementService) : ObservableObject
{
    [ObservableProperty]
    private TemperatureMeasurement? _currentMeasurement;
    
    public ObservableCollection<TemperatureMeasurement> Measurements { get; } = new();

    [RelayCommand]
    private async Task StartMeasurementAsync(CancellationToken cancellationToken)
    {
        await foreach (var actMeasurement in measurementService.StartMeasurement(cancellationToken))
        {
            this.CurrentMeasurement = actMeasurement;
            
            this.Measurements.Insert(0, actMeasurement);
            if (this.Measurements.Count > 20)
            {
                this.Measurements.RemoveAt(this.Measurements.Count - 1);
            }
        }
    }
}