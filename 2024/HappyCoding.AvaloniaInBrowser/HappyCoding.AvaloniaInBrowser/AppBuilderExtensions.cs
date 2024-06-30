using Avalonia;
using HappyCoding.AvaloniaInBrowser.Services;
using HappyCoding.AvaloniaInBrowser.Views;
using Microsoft.Extensions.DependencyInjection;
using RolandK.AvaloniaExtensions.DependencyInjection;

namespace HappyCoding.AvaloniaInBrowser;

public static class AppBuilderExtensions
{
    public static AppBuilder AddAvaloniaInBrowser(this AppBuilder appBuilder) =>
        appBuilder.UseDependencyInjection(services =>
        {
            // Services
            services.AddSingleton<ITestDataGenerator, BogusTestDataGenerator>();
                
            // ViewModels
            services.AddTransient<MainViewViewModel>();
        });
}