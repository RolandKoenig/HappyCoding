using Grpc.Net.Client;
using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;

namespace HappyCoding.GrpcCommunicationFeatures.ConsoleClient;

public static class Program
{
    public static async Task Main()
    {
        // await CallSayHello_TheShortWay_Async();
        await CallSayHello_WithSocketHttpHandlerConfig_Async();
    }

    private static async Task CallSayHello_TheShortWay_Async()
    {
        using var channel = GrpcChannel.ForAddress($"http://localhost:5000");

        Console.WriteLine("Connecting...");
        await channel.ConnectAsync();

        var greeterClient = new Greeter.GreeterClient(channel);
        for (var loop = 0; loop < 10; loop++)
        {
            var actRequest = new HelloRequest()
            {
                Name = $"Console loop {loop + 1}"
            };

            Console.WriteLine($"Say hello #{loop + 1}");
            await greeterClient.SayHelloAsync(actRequest);
        }
    }

    private static async Task CallSayHello_WithSocketHttpHandlerConfig_Async()
    {
        var socketsHttpHandler = new SocketsHttpHandler();
        socketsHttpHandler.PooledConnectionIdleTimeout = TimeSpan.FromSeconds(5);

        var grpcChannelOptions = new GrpcChannelOptions()
        {
            HttpHandler = socketsHttpHandler,
        };
        using var channel = GrpcChannel.ForAddress($"http://localhost:5000", grpcChannelOptions);

        Console.WriteLine("Connecting...");
        await channel.ConnectAsync();

        var greeterClient = new Greeter.GreeterClient(channel);
        for (var loop = 0; loop < 10; loop++)
        {
            var actRequest = new HelloRequest()
            {
                Name = $"Console loop {loop + 1}"
            };

            Console.WriteLine($"Say hello #{loop + 1}");
            await greeterClient.SayHelloAsync(actRequest);

            await Task.Delay(100);
        }
    }
}
