using Microsoft.Extensions.Logging;

namespace HappyCoding.GrpcKubernetesLoadBalancing.Client;

public class ServerCallingLoop
{
    private readonly ILogger _logger;
    private readonly Greeter.GreeterClient _greeterClient;

    public ServerCallingLoop(
        ILogger<ServerCallingLoop> logger,
        Greeter.GreeterClient greeterClient)
    {
        _logger = logger;
        _greeterClient = greeterClient;
    }
    
    public async Task RunAsync()
    {
        while (true)
        {
            try
            {
                await _greeterClient.SayHelloAsync(new HelloRequest() { Name = "Client" });
                _logger.LogInformation("Successfully called Server");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while calling ");
            }
            
            await Task.Delay(500)
                .ConfigureAwait(false);
        }
    }
}