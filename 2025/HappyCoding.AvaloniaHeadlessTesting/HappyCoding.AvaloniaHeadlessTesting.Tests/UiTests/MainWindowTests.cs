using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using HappyCoding.AvaloniaHeadlessTesting.Tests.Util;

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
    public void Navigation_should_select_HomeView_initially()
    {
        // Arrange / Act
        var window = new MainWindow();
        window.Show();
        
        // Assert
        var selectedNavigationItem = window.LocateByTestId("Navigation").LocateBySelection();
        var textBlock = selectedNavigationItem.LocateByType<TextBlock>();
        
        Assert.Equal("Home", textBlock.Text);
    }

    [AvaloniaTheory]
    [InlineData("Counter", "Counter")]
    [InlineData("Weather", "Weather Forecast")]
    public void Navigation_should_be_able_to_navigate_to(string viewDisplayName, string viewHeader)
    {
        // Arrange
        var window = new MainWindow();
        window.Show();
        
        // Act
        window.LocateByTestId("Navigation").LocateByText(viewDisplayName).SimulateClick();
        
        // Assert
        var currentHeader = window.LocateByTestId("MainContent").LocateByClass("H2");
        var currentHeaderText = currentHeader.ReadTextFromVisual();
        Assert.Equal(viewHeader, currentHeaderText);
    }
}