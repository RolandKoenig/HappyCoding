using CommunityToolkit.Mvvm.Messaging;
using HappyCoding.AvaloniaWithMapsui.MapsModule.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.AvaloniaWithMapsui.MapsModule;

public static class Bootstrap
{
    public static void Load(IServiceCollection services, IMessenger messenger)
    {
        // ViewModels
        services.AddTransient<MapsViewModel>();
    }
}