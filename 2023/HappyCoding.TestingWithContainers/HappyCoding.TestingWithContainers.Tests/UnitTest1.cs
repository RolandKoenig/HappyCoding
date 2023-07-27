using DotNet.Testcontainers.Builders;

namespace HappyCoding.TestingWithContainers.Tests;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        // Create a new instance of a container.
        await using var container = new ContainerBuilder()
            .WithImage("testcontainers/helloworld:1.1.0")
            .WithPortBinding(8080, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPort(8080)))
            .Build();

        // Start the container.
        await container.StartAsync()
            .ConfigureAwait(false);

        // Create a new instance of HttpClient to send HTTP requests.
        var httpClient = new HttpClient();

        // Construct the request URI by specifying the scheme, hostname, assigned random host port, and the endpoint "uuid".
        var requestUri = new UriBuilder(Uri.UriSchemeHttp, container.Hostname, container.GetMappedPublicPort(8080), "uuid").Uri;

        // Send an HTTP GET request to the specified URI and retrieve the response as a string.
        var guid = await httpClient.GetStringAsync(requestUri)
            .ConfigureAwait(false);
    }
}