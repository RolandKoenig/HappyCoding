using CommunityToolkit.Mvvm.Messaging;
using HappyCoding.AvaloniaWithMapsui.FilesModule.Adapter;
using HappyCoding.AvaloniaWithMapsui.FilesModule.Interface;
using HappyCoding.AvaloniaWithMapsui.FilesModule.Services;
using HappyCoding.AvaloniaWithMapsui.FilesModule.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.AvaloniaWithMapsui.FilesModule;

public static class Bootstrap
{
    public static void Load(IServiceCollection services, IMessenger messenger)
    {
        // Services
        services.AddSingleton<GpxFileRepository>();
        services.AddSingleton<IGpxFileRepositoryAdapter, GpxFileRepositoryAdapter>();

        // ViewModels
        services.AddTransient<LoadedGpxFilesViewModel>();
    }
}