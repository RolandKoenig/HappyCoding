using CommunityToolkit.Mvvm.Messaging;
using HappyCoding.AvaloniaWithMapsui.SelectionModule.Domain;
using HappyCoding.AvaloniaWithMapsui.SelectionModule.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.AvaloniaWithMapsui.SelectionModule;

public static class Bootstrap
{
    public static void Load(IServiceCollection services, IMessenger messenger)
    {
        services.AddSingleton<IGpxFileSelectionManager, GpxFileSelectionManager>();
    }
}