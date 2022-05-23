using System;
using HappyCoding.SimpleWinUI3App.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace HappyCoding.SimpleWinUI3App;

internal class Startup
{
    internal static IServiceProvider ConfigureServices(Window mainWindow)
    {
        var services = new ServiceCollection();

        services.AddSingleton<IUserRepository, DummyUserRepository>();

        return services.BuildServiceProvider();
    }
}
