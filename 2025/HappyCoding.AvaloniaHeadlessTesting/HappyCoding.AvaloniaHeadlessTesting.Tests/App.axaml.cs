using Avalonia;
using Avalonia.Markup.Xaml;

namespace HappyCoding.AvaloniaHeadlessTesting.Tests;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }
}