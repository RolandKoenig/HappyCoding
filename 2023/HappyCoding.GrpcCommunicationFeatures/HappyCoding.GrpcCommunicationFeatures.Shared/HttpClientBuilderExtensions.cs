using HappyCoding.GrpcCommunicationFeatures.Shared.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.GrpcCommunicationFeatures.Shared;

public static class HttpClientBuilderExtensions
{
    /// <summary>
    /// Logs all outgoing http calls.
    /// </summary>
    public static IHttpClientBuilder AddLoggingForOutgoingHttpCalls(this IHttpClientBuilder builder)
        => builder.AddHttpMessageHandler<LoggingDelegatingHandler>();
}
