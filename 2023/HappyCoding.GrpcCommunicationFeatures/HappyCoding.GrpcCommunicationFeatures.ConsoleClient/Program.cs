using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;

namespace HappyCoding.GrpcCommunicationFeatures.ConsoleClient;

public static class Program
{
    public static async Task Main()
    {
        // using var channel = GrpcSetup.SetupGrpcChannel();
        // using var channel = GrpcSetup.SetupGrpcChannelWithSocketHttpHandlerConfig();
        using var channel = GrpcSetup.SetupGrpcChannelWithLoadBalancing();

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

            await Task.Delay(500);
        }
    }
}
