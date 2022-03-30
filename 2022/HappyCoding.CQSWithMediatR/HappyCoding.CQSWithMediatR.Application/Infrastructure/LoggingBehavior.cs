using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HappyCoding.CQSWithMediatR.Application.Infrastructure;

// Logging pipeline behavior
// Based on https://garywoodfine.com/how-to-use-mediatr-pipeline-behaviours/

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        // Request
        _logger.LogInformation($"Handling {typeof(TRequest).Name}");

        var stopWatch = new Stopwatch();
        stopWatch.Start();
        try
        {
            return await next();
        }
        finally
        {
            _logger.LogInformation($"Handled {typeof(TResponse).Name} in {stopWatch.Elapsed.TotalMilliseconds:F2} ms");
        }
    }
}
