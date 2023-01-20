using System;
using Avalonia;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.CommunityToolkitMvvm.DependencyInjection;

public static class AppBuilderExtensions
{
    public static AppBuilder UseDependencyInjection(this AppBuilder appBuilder, Action<IServiceCollection> registerServicesAction)
    {
        appBuilder.AfterSetup(x =>
        {
            var services = new ServiceCollection();

            registerServicesAction(services);
            
            x.Instance.Resources.Add(
                DependencyInjectionConstants.SERVICE_PROVIDER_RESOURCE_KEY,
                services.BuildServiceProvider());
        });

        return appBuilder;
    }
}