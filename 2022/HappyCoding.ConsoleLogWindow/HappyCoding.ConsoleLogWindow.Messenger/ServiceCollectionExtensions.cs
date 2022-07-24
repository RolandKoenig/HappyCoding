using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.ConsoleLogWindow.Messenger;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFirLibMessenger(this IServiceCollection services)
    {
        var messenger = new FirLibMessenger();
        messenger.ConnectToGlobalMessaging(
                FirLibMessengerThreadingBehavior.EnsureMainSyncContextOnSyncCalls,
                "GUI",
                SynchronizationContext.Current);
        services.AddSingleton<IFirLibMessagePublisher>(_ => messenger);
        services.AddSingleton<IFirLibMessageSubscriber>(_ => messenger);

        return services;
    }
}
