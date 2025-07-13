using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using HappyCoding.TemperatureViewer.Services;

namespace HappyCoding.TemperatureViewer;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var mainViewModel = new MainViewModel(new RandomMeasurementService());
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainWindow = new MainWindow();
            mainWindow.DataContext = mainViewModel;
            
            desktop.MainWindow = mainWindow;
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewApplicationLifetime)
        {
            var mainView = new MainView();
            mainView.DataContext = mainViewModel;
            
            singleViewApplicationLifetime.MainView = mainView;
        }

        base.OnFrameworkInitializationCompleted();
    }
}