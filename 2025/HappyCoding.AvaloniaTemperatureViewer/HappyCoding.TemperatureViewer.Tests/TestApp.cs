using System.Globalization;
using Avalonia;
using Avalonia.Headless;

[assembly: AvaloniaTestApplication(
    typeof(HappyCoding.TemperatureViewer.Tests.TestApp))]

namespace HappyCoding.TemperatureViewer.Tests;

public static class TestApp
{
    public static AppBuilder BuildAvaloniaApp()
    {
        var cultureEn = new CultureInfo("en");
        CultureInfo.CurrentCulture = cultureEn;
        CultureInfo.CurrentUICulture = cultureEn;
        
        return Program.BuildAvaloniaApp()
            .UseHeadless(new AvaloniaHeadlessPlatformOptions());
    }
}