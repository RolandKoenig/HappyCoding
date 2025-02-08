using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Headless.XUnit;
using Avalonia.Input;

namespace HappyCoding.AvaloniaHeadlessTesting.Tests.UiTests;

public class MainWindowTests
{
    [AvaloniaFact]
    public void Should_have_correct_Title()
    {
        // Arrange / Act
        var window = new MainWindow();
        window.Show();

        // Assert
        Assert.Equal("Avalonia App", window.Title);
    }
    
    [AvaloniaFact]
    public void Should_process_int_input()
    {
        // Arrange
        var window = new MainWindow();
        window.Show();
        
        // Act
        var inputControl = window.GetControl<Control>("TxtInputInt");
        
        inputControl.Focus();
        window.KeyPressQwerty(PhysicalKey.Delete, RawInputModifiers.None);
        window.KeyReleaseQwerty(PhysicalKey.Delete, RawInputModifiers.None);
        window.KeyTextInput("5");
        
        // Assert
        var viewModel = (MainWindowViewModel)window.DataContext!;
        Assert.Equal(5, viewModel.InputInt);
    }
    
    [AvaloniaFact]
    public void Should_process_invalid_input()
    {
        // Arrange
        var window = new MainWindow();
        window.Show();
        
        // Act
        var inputControl = window.GetControl<Control>("TxtInputInt");

        inputControl.Focus();
        window.KeyPressQwerty(PhysicalKey.Delete, RawInputModifiers.None);
        window.KeyReleaseQwerty(PhysicalKey.Delete, RawInputModifiers.None);
        window.KeyTextInput("abc");
        
        // Assert
        var viewModel = (MainWindowViewModel)window.DataContext!;
        Assert.Equal(0, viewModel.InputInt);
    }
}