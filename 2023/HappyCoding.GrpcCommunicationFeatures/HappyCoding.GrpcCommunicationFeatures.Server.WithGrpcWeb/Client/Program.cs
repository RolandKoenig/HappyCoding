using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using HappyCoding.GrpcCommunicationFeatures.ProtoDefinition;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace HappyCoding.GrpcCommunicationFeatures.Server.WithGrpcWeb.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddHttpClient("HappyCoding.GrpcCommunicationFeatures.Server.WithGrpcWeb.ServerAPI",
            client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

        // Supply HttpClient instances that include access tokens when making requests to the server project
        builder.Services.AddScoped(sp =>
            sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("HappyCoding.GrpcCommunicationFeatures.Server.WithGrpcWeb.ServerAPI"));
        
        builder.Services.AddSingleton(services =>
        {
            // Create a gRPC-Web channel pointing to the backend server
            var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
            var baseUri = services.GetRequiredService<NavigationManager>().BaseUri;
            var channel = GrpcChannel.ForAddress(baseUri, new GrpcChannelOptions { HttpClient = httpClient });

            // Now we can instantiate gRPC clients for this channel
            return new Greeter.GreeterClient(channel);
        });
        
        await builder.Build().RunAsync();
    }
}

