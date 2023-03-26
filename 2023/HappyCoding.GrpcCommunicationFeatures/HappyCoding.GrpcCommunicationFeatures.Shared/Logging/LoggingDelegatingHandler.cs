using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace HappyCoding.GrpcCommunicationFeatures.Shared.Logging;

internal class LoggingDelegatingHandler : DelegatingHandler
{
    private readonly ILogger _logger;

    public LoggingDelegatingHandler(ILogger<LoggingDelegatingHandler> logger)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var method = request.Method;
        var requestUri = request.RequestUri;

        _logger.LogInformation("Starting outgoing {HttpMethod} to {HttpUrl}", method, requestUri);

        var stopWatch = Stopwatch.StartNew();
        try
        {
            var response = await base.SendAsync(request, cancellationToken);
            stopWatch.Stop();

            _logger.LogInformation(
                "Finished outgoing {HttpMethod} to {HttpUrl} in {Time:F2} ms with result {ResultCode} {ResultCodeText}",
                method, requestUri,
                stopWatch.Elapsed.TotalMilliseconds,
                (int)response.StatusCode,
                response.StatusCode);

            return response;
        }
        catch (HttpRequestException requestEx) when (requestEx.StatusCode.HasValue)
        {
            stopWatch.Stop();

            _logger.LogError(
                "Finished outgoing {HttpMethod} to {HttpUrl} in {Time:F2} ms with result {ResultCode} {ResultCodeText}",
                method, requestUri,
                stopWatch.Elapsed.TotalMilliseconds,
                (int)requestEx.StatusCode,
                requestEx.StatusCode);

            throw;
        }
        catch (TaskCanceledException)
        {
            stopWatch.Stop();

            _logger.LogError(
                "Finished outgoing {HttpMethod} to {HttpUrl} in {Time:F2} ms with timeout",
                method, requestUri,
                stopWatch.Elapsed.TotalMilliseconds);

            throw;
        }
        catch (Exception ex)
        {
            stopWatch.Stop();

            _logger.LogError(
                "Finished outgoing {HttpMethod} to {HttpUrl} in {Time:F2} ms unhandled exception of type {ExceptionType}",
                method, requestUri,
                stopWatch.Elapsed.TotalMilliseconds,
                ex.GetType().FullName);

            throw;
        }
    }
}
