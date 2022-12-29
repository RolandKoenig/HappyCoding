using System;
using System.Net.Http;
using Grpc.Net.Client;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels.Grpc;

internal static class GrpcHelper
{
    public static GrpcChannel BuildChannel(ClientOptions options)
    {
        var socketsHttpHandler = new SocketsHttpHandler();
        socketsHttpHandler.PooledConnectionIdleTimeout = 
            TimeSpan.FromMilliseconds(options.PooledConnectionIdleTimeoutMS);
        socketsHttpHandler.EnableMultipleHttp2Connections = true;
        socketsHttpHandler.SslOptions.RemoteCertificateValidationCallback =
            (httpRequestMessage, cert, cetChain, policyErrors) => true;

        var grpcChannelOptions = new GrpcChannelOptions()
        {
            HttpHandler = socketsHttpHandler,
        };

        var protocol = options.UseHttps ? "https" : "http";
        var channel = GrpcChannel.ForAddress($"{protocol}://{options.TargetHost}:{options.PortHttp2}", grpcChannelOptions);

        return channel;
    }
}
