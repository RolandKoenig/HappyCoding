using System;
using Grpc.Net.Client;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using HappyCoding.GRpcCommunication.Shared.Services;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels;

internal class SimpleRequestReplyWithStringChannel : ITestChannel
{
    private GrpcChannel? _channel;

    /// <inheritdoc />
    public bool IsConnected => _channel?.State == ConnectivityState.Ready ? true : false;

    /// <inheritdoc />
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var options = await ClientOptions.LoadAsync(cancellationToken);

        var protocol = options.UseHttps ? "https" : "http";

        _channel = GrpcChannel.ForAddress($"{protocol}://{options.TargetHost}:{options.Port}");

        this.Run(_channel);
    }

    /// <inheritdoc />
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _channel?.Dispose();

        return Task.CompletedTask;
    }

    private async void Run(GrpcChannel channel)
    {
        while (channel == _channel)
        {
            await Task.Delay(100)
                .ConfigureAwait(false);

            switch (channel.State)
            {
                case ConnectivityState.Connecting:
                case ConnectivityState.Shutdown:
                case ConnectivityState.TransientFailure:
                    continue;

                default:
                    // Try to send
                    break;
            }

            try
            {
                var client = new SimpleRequestReplyWithStringHandler.SimpleRequestReplyWithStringHandlerClient(channel);
                client.Handle(new SimpleRequestWithString() {Name = "Test"});
            }
            catch (Exception)
            {
                // Nothing todo
            }
        }
    }
}
