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
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainWindow = new MainWindow();
            mainWindow.DataContext = 
                new TemperatureViewerViewModel(new RandomMeasurementService());

            desktop.MainWindow = mainWindow;
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleView)
        {
            var mainView = new MainSingleView();
            mainView.DataContext = new TemperatureViewerViewModel(new RandomMeasurementService());
            singleView.MainView = mainView;
        }
        
        base.OnFrameworkInitializationCompleted();
    }
}