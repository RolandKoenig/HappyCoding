using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using HappyCoding.GRpcCommunication.Shared.SimpleRequest;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RolandK.Patterns.Messaging;

namespace HappyCoding.GRpcCommunication.ServerApp.ServerHost.GRpc;

public class SimpleRequestHandlerService : SimpleRequestHandler.SimpleRequestHandlerBase
{
    private readonly ServerOptions _options;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;
    private readonly ILogger _logger;

    public SimpleRequestHandlerService(
        ServerOptions options,
        IHostApplicationLifetime hostApplicationLifetime,
        ILogger<SimpleRequestHandlerService> logger)
    {
        _options = options;
        _hostApplicationLifetime = hostApplicationLifetime;
        _logger = logger;
    }

    /// <inheritdoc />
    public override async Task<SimpleResponse> Handle(SimpleRequest request, ServerCallContext context)
    {
        if (_options.SimulatedProcessingTimeMS > 0)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(_options.SimulatedProcessingTimeMS));
        }

        return new SimpleResponse()
        {
            Message = "Test message"
        };
    }

    /// <inheritdoc />
    public override async Task HandleStreamed(
        IAsyncStreamReader<SimpleRequest> requestStream, 
        IServerStreamWriter<SimpleResponse> responseStream, ServerCallContext context)
    {
        try
        {
            using var cancelSource = CancellationTokenSource.CreateLinkedTokenSource(
                context.CancellationToken,
                _hostApplicationLifetime.ApplicationStopping);
            var cancelToken = cancelSource.Token;

            await foreach (var actRequest in requestStream.ReadAllAsync(cancelToken))
            {
                if (_options.SimulatedProcessingTimeMS > 0)
                {
                    await Task.Delay(
                        TimeSpan.FromMilliseconds(_options.SimulatedProcessingTimeMS),
                        cancelToken);
                }

                if (cancelToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Request/response stream cancelled");
                    break;
                }

                await responseStream.WriteAsync(new SimpleResponse()
                {
                    Message = "Test message"
                });
            }
        }
        catch (TaskCanceledException)
        {
            _logger.LogInformation("Request/response stream cancelled");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while handing request/response stream");
        }
    }
}
