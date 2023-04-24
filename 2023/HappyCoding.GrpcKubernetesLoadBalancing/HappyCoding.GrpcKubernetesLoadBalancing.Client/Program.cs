using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HappyCoding.GrpcKubernetesLoadBalancing.Client;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var services = new ServiceCollection();
        services.AddLogging(builder => builder.AddConsole());
        services.AddGrpcClient<Greeter.GreeterClient>(options =>
        {
            var targetEndpoint = configuration["TargetEndpoint"]!;
            Console.WriteLine($"Connect to target {targetEndpoint}");
            
            options.Address = new Uri(targetEndpoint);
            options.ChannelOptionsActions.Add(options =>
            {
                options.Credentials = ChannelCredentials.Insecure;
                options.ServiceConfig = new ServiceConfig()
                {
                    LoadBalancingConfigs =
                    {
                        new RoundRobinConfig()
                    }
                };
            });
        });
        services.AddSingleton<ServerCallingLoop>();

        var serviceProvider = services.BuildServiceProvider();
        var loop = serviceProvider.GetRequiredService<ServerCallingLoop>();

        await loop.RunAsync();
    }
}