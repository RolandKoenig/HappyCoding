using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using HappyCoding.TemperatureViewer.Model;
using HappyCoding.TemperatureViewer.Services;
using HappyCoding.TemperatureViewer.Tests.Util;
using NSubstitute;

namespace HappyCoding.TemperatureViewer.Tests;

public class MainWindowTests
{
    [AvaloniaFact]
    public void StartTemparatureMeasurement()
    {
        // Arrange
        var testData = new[]
        {
            new TemperatureMeasurement(DateTimeOffset.UtcNow, 10.5)
        };
        
        var mockedMeasurementService = Substitute.For<IMeasurementService>();
        mockedMeasurementService.StartMeasurement(Arg.Any<CancellationToken>())
            .Returns(_ => testData.ToAsyncEnumerable());
        
        var mainWindow = new MainWindow();
        mainWindow.DataContext = new MainViewModel(mockedMeasurementService);
        mainWindow.Show();
        
        // Act
        var mnuStartMeasuring = mainWindow.GetControl<MenuItem>("MnuStartMeasuring");

        mnuStartMeasuring.SimulateClick();
        
        // Assert
        var txtCurrentTemperature = mainWindow.GetControl<TextBlock>("TxtCurrentTemperature");
        Assert.Equal("10.50°", txtCurrentTemperature.Text);
    }
}