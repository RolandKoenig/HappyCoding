using System.IO.Pipes;

namespace HappyCoding.AspNetCoreWithNamedPipes.Client;

// Original code from
// https://andrewlock.net/using-named-pipes-with-aspnetcore-and-httpclient/

public static class Program
{
    public static async Task Main(string[] args)
    {
        var httpHandler = new SocketsHttpHandler
        {
            // Called to open a new connection
            ConnectCallback = async (_, cancellationToken) =>
            {
                // Configure the named pipe stream
                var pipeClientStream = new NamedPipeClientStream(
                    serverName: ".",
                    pipeName: "my-test-pipe",
                    PipeDirection.InOut,
                    PipeOptions.Asynchronous);

                // Connect to the server!
                await pipeClientStream.ConnectAsync(cancellationToken);
        
                return pipeClientStream;
            }
        };

        // Create an HttpClient using the named pipe handler
        var httpClient = new HttpClient(httpHandler)
        {
            BaseAddress = new Uri("https://localhost")
        };

        var result = await httpClient.GetStringAsync("/weatherforecast");
        Console.WriteLine(result);
    }
}