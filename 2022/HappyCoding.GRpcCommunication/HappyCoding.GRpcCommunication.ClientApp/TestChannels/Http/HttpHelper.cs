using System;
using System.Net.Http;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels.Http;

internal class HttpHelper
{
    public static HttpClient BuildHttpClient(ClientOptions options, Version httpVersion, ushort port)
    {
        var protocol = options.UseHttps ? "https" : "http";

        var handler = new SocketsHttpHandler();
        handler.PooledConnectionIdleTimeout = TimeSpan.FromMilliseconds(options.PooledConnectionIdleTimeoutMS);
        handler.EnableMultipleHttp2Connections = true;
        handler.SslOptions.RemoteCertificateValidationCallback =
            (httpRequestMessage, cert, cetChain, policyErrors) => true;

        var httpClient = new HttpClient(handler);
        httpClient.BaseAddress = new Uri($"{protocol}://{options.TargetHost}:{port}");
        httpClient.Timeout = TimeSpan.FromMilliseconds(options.CallTimeoutMS);
        httpClient.DefaultRequestVersion = httpVersion;
        httpClient.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact;
        return httpClient;
    }
}
