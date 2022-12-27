namespace HappyCoding.HttpClientServerCalls.Client.Requests;

/* Produces http headers:
 *
 * Host: localhost:5001
 *
 */

internal class Get : IClientRequest
{
    public async Task ExecuteAsync(HttpClient client)
    {
        await client.GetAsync("dummyEndpoint");
    }
}