using Avalonia;
using Avalonia.Headless;

[assembly: AvaloniaTestApplication(
    typeof(HappyCoding.AvaloniaHeadlessTesting.Tests.TestAppBuilder))]

namespace HappyCoding.AvaloniaHeadlessTesting.Tests;

public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
        .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}