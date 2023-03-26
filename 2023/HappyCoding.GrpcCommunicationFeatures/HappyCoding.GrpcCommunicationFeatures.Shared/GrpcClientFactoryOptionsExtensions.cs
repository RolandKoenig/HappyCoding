using Grpc.Net.ClientFactory;

namespace HappyCoding.GrpcCommunicationFeatures.Shared;

public static class GrpcClientFactoryOptionsExtensions
{
    /// <summary>
    /// Helper method to get access to the <see cref="SocketsHttpHandler"/> for some detail configuration.
    /// </summary>
    public static GrpcClientFactoryOptions ConfigureSocketHttpHandler(
        this GrpcClientFactoryOptions options,
        Action<SocketsHttpHandler> configureAction)
    {
        options.ChannelOptionsActions.Add(channelOptions =>
        {
            var socketHttpHandler = FindSocketHttpHandler(channelOptions.HttpHandler);
            configureAction(socketHttpHandler);
        });

        return options;
    }

    private static SocketsHttpHandler FindSocketHttpHandler(HttpMessageHandler? messageHandler)
    {
        var actDelegatingHandler = messageHandler as DelegatingHandler;
        while (actDelegatingHandler != null)
        {
            messageHandler = actDelegatingHandler.InnerHandler;
            actDelegatingHandler = messageHandler as DelegatingHandler;
        }

        if (messageHandler is not SocketsHttpHandler socketHttpHandler)
        {
            throw new InvalidOperationException("Unable to find SocketHttpHandler!");
        }

        return socketHttpHandler;
    }
}
