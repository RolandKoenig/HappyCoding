using Avalonia.Headless.XUnit;
using HappyCoding.AvaloniaHeadlessTesting.Tests.Util;

namespace HappyCoding.AvaloniaHeadlessTesting.Tests.UiTests;

public class CounterViewTests
{
    [AvaloniaTheory]
    [InlineData(0, "0")]
    [InlineData(1, "1")]
    [InlineData(5, "5")]
    public void CounterView_should_increment_correctly(int incrementClickCount, string expectedCounterText)
    {
        // Arrange
        var window = new MainWindow();
        window.Show();
        
        window
            .LocateByTestId("Navigation")
            .LocateByText("Counter")
            .SimulateClick();
        var mainContent = window.LocateByTestId("MainContent");
        
        // Act
        var counterButton = mainContent.LocateByText("Increment");
        for (var loop = 0; loop < incrementClickCount; loop++)
        {
            counterButton.SimulateClick();
        }
        
        // Assert
        mainContent.LocateByText(expectedCounterText);
    }
}