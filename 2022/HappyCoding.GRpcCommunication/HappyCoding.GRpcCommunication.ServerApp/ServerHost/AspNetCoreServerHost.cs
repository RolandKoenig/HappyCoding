using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using HappyCoding.GRpcCommunication.ServerApp.Messages;
using HappyCoding.GRpcCommunication.ServerApp.ServerHost.GRpc;
using HappyCoding.GRpcCommunication.ServerApp.ServerHost.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RolandK.Patterns.Messaging;

namespace HappyCoding.GRpcCommunication.ServerApp.ServerHost;

internal class AspNetCoreServerHost
{
    private WebApplication? _app;
    private FirLibMessenger? _serverMessenger;

    public bool IsStarted => _app != null;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (_app != null)
        {
            throw new InvalidOperationException($"Unable to start {nameof(AspNetCoreServerHost)}: It is already started!");
        }

        var options = await ServerOptions.LoadAsync(cancellationToken);

        _serverMessenger = new FirLibMessenger();
        _serverMessenger.ConnectToGlobalMessaging(
            FirLibMessengerThreadingBehavior.Ignore,
            ServerConstants.SERVER_MESSENGER_NAME,
            null);

        var builder = WebApplication.CreateBuilder(Array.Empty<string>());

        builder.WebHost.ConfigureKestrel(serverOptions =>
        {
            serverOptions.Listen(IPAddress.Any, options.Port, kestrelOptions =>
            {
                kestrelOptions.Protocols = HttpProtocols.Http2;

                if (options.UseHttps) { kestrelOptions.UseHttps(); }
            });
        });

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

        // Add services to the container.
        builder.Services.AddGrpc();
        builder.Services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddProvider(new ViewLoggerProvider(_serverMessenger));
        });
        builder.Services.AddSingleton<IFirLibMessagePublisher>(_serverMessenger);

        builder.Services.AddSingleton(options);

        var app = builder.Build();

        // Add gRPC services
        app.MapGrpcService<SimpleRequestHandlerService>();
        app.MapGrpcService<ComplexRequestHandlerService>();

        // Add http endpoints
        app.MapGet(
            "/", (
            ) => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
        app.MapSimpleRequestEndpoint();
        app.MapComplexRequestEndpoint();

        await app.StartAsync(cancellationToken);

        _app = app;
        _serverMessenger.Publish(new ServerStateChangedMessage(true));
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_app == null)
        {
            throw new InvalidOperationException($"Unable to stop {nameof(AspNetCoreServerHost)}: It is not started!");
        }

        await _app.StopAsync(cancellationToken);

        _serverMessenger!.Publish(new ServerStateChangedMessage(false));
        _serverMessenger.DisconnectFromGlobalMessaging();
        _serverMessenger = null;

        _app = null;
        
    }
}
