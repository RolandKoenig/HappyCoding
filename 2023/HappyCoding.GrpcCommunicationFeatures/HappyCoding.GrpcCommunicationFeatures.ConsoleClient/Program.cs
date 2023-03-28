using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Client.Balancer;
using Grpc.Net.Client.Configuration;
using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;
using HappyCoding.GrpcCommunicationFeatures.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace HappyCoding.GrpcCommunicationFeatures.ConsoleClient;

public static class Program
{
    public static async Task Main()
    {
        // await CallSayHello_TheShortWay_Async();
        // await CallSayHello_WithSocketHttpHandlerConfig_Async();
        await CallSayHello_WithStaticLoadBalancing_Async();
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

    private static async Task CallSayHello_WithStaticLoadBalancing_Async()
    {
        var services = new ServiceCollection();
        services.AddGrpcStaticLoadBalancingForScheme(
            "happycoding-srv",
            new BalancerAddress("localhost", 5000),
            new BalancerAddress("localhost", 5001),
            new BalancerAddress("localhost", 5002),
            new BalancerAddress("localhost", 5003));

        using var channel = GrpcChannel.ForAddress(
            "happycoding-srv://myservice",
            new GrpcChannelOptions()
            {
                Credentials = ChannelCredentials.Insecure,
                ServiceProvider = services.BuildServiceProvider(),
                ServiceConfig = new ServiceConfig()
                {
                    LoadBalancingConfigs =
                    {
                        new RoundRobinConfig()
                    }
                }
            });

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
}
