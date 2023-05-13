using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using HappyCoding.GrpcWithNet48.ProtoDefinitions;

namespace HappyCoding.GrpcWithNet48.Net48Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient(
                new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));

            var baseUri = new Uri("http://localhost:5000");
            var channel = GrpcChannel.ForAddress(baseUri, new GrpcChannelOptions { HttpClient = httpClient });

            var grpcClient = new Greeter.GreeterClient(channel);

            for (var loop = 0; loop < 10; loop++)
            {
                var actName = $"Net48 Client {(loop + 1)}";

                Console.WriteLine($"Sending hello from {actName}");
                var response = await grpcClient.SayHelloAsync(
                    new HelloRequest()
                    {
                        Name = actName
                    });

                Console.WriteLine($"Received: {response.Message}");

                Console.WriteLine();
                await Task.Delay(300);
            }
        }
    }
}
