using Avalonia.Headless.XUnit;
using HappyCoding.AvaloniaHeadlessTesting.Tests.Util.Actions;
using HappyCoding.AvaloniaHeadlessTesting.Tests.Util.Locators;

namespace HappyCoding.AvaloniaHeadlessTesting.Tests.UiTests;

public class WeatherForecastViewTests
{
    [AvaloniaFact]
    public async Task WeatherForecastGrid_should_have_entry_for_tomorrow()
    {
        // Arrange
        var window = new MainWindow();
        window.Show();

        await window.LocateByTestId("Navigation").ThenByText("Weather").ClickAsync();

        // Act
        await window.LocateByTestId("WeatherForecastGrid").ShouldBeVisibleAsync();
        
        // Assert
        var expectedDateString = DateOnly.FromDateTime(DateTime.Now.AddDays(1)).ToString();
        await window.LocateByTestId("WeatherForecastGrid").ShouldHaveTextAsync(expectedDateString);
    }
}