using Avalonia.Headless.XUnit;
using HappyCoding.AvaloniaHeadlessTesting.Tests.Util.Actions;
using HappyCoding.AvaloniaHeadlessTesting.Tests.Util.Locators;

namespace HappyCoding.AvaloniaHeadlessTesting.Tests.UiTests;

public class CounterViewTests
{
    [AvaloniaTheory]
    [InlineData(0, "0")]
    [InlineData(1, "1")]
    [InlineData(5, "5")]
    public async Task CounterView_should_increment_correctly(int incrementClickCount, string expectedCounterText)
    {
        // Arrange
        var window = new MainWindow();
        window.Show();

        await window.LocateByTestId("Navigation").ThenByText("Counter").ClickAsync();

        // Act
        var incrementButtonLocator = window.LocateByTestId("MainContent").ThenByText("Increment");
        for (var loop = 0; loop < incrementClickCount; loop++)
        {
            await incrementButtonLocator.ClickAsync();
        }
        
        // Assert
        await window.LocateByTestId("MainContent").ThenByText(expectedCounterText).ShouldExistAsync();
    }
}