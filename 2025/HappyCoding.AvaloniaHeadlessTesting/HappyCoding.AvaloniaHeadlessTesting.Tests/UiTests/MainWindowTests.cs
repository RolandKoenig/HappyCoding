using Avalonia.Headless.XUnit;
using HappyCoding.AvaloniaHeadlessTesting.Tests.Util.Actions;
using HappyCoding.AvaloniaHeadlessTesting.Tests.Util.Locators;

namespace HappyCoding.AvaloniaHeadlessTesting.Tests.UiTests;

public class MainWindowTests
{
    [AvaloniaFact]
    public void MainWindow_should_have_correct_Title()
    {
        // Arrange / Act
        var window = new MainWindow();
        window.Show();

        // Assert
        Assert.Equal("HappyCoding AvaloniaHeadlessTesting", window.Title);
    }

    [AvaloniaFact]
    public async Task Navigation_should_select_HomeView_initially()
    {
        // Arrange / Act
        var window = new MainWindow();
        window.Show();
        
        // Assert
        await window.LocateByTestId("Navigation").ThenBySelection().ShouldHaveTextAsync("Home");
    }

    [AvaloniaTheory]
    [InlineData("Counter", "Counter")]
    [InlineData("Weather", "Weather Forecast")]
    public async Task Navigation_should_be_able_to_navigate_to(string viewDisplayName, string viewHeader)
    {
        // Arrange
        var window = new MainWindow();
        window.Show();
        
        // Act
        await window.LocateByTestId("Navigation").ThenByText(viewDisplayName).ClickAsync();
        
        // Assert
        await window.LocateByTestId("MainContent").ThenByClass("H2").ShouldHaveTextAsync(viewHeader);
    }
}