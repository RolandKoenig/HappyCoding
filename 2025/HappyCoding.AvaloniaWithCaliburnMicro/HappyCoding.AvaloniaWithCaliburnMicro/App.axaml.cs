using Avalonia;
using Avalonia.Markup.Xaml;

namespace HappyCoding.AvaloniaWithCaliburnMicro;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        base.OnFrameworkInitializationCompleted();
        new Bootstrapper();
    }
}