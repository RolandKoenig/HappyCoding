namespace HappyCoding.HttpClientServerCalls.Client.Requests;

/* Produces http headers:
 *
 * Host: localhost:5001
 *
 */

internal class Delete : IClientRequest
{
    public async Task ExecuteAsync(HttpClient client)
    {
        await client.DeleteAsync("dummyEndpoint");
    }
}