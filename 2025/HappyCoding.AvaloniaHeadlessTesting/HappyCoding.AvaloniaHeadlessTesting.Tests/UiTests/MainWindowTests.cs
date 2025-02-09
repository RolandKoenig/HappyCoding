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
        var lstNavigation = window.LocateLogicalChildOfTypeWithName<ListBox>("LstNavigation");
        var selectedListBoxItem = lstNavigation.LocateSelectedItem();
        Assert.NotNull(selectedListBoxItem);

        var textBlock = selectedListBoxItem.LocateLogicalChildOfType<TextBlock>();
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
        var lstNavigation = window.LocateLogicalChildOfTypeWithName<ListBox>("LstNavigation");
        var item = lstNavigation.LocateLogicalChildTextBlockWithText(viewDisplayName);
        item.SimulateClick(window);
        
        // Assert
        var currentHeader = window.LocateLogicalChildOfTypeWithClass<TextBlock>("H2");
        Assert.Equal(viewHeader, currentHeader.Text);
    }
}