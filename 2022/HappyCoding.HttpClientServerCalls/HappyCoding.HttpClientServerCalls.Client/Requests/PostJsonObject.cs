using System.Net.Http.Json;

namespace HappyCoding.HttpClientServerCalls.Client.Requests;

/* Produces http headers:
 *
 * Host: localhost:5001
 * Content-Type: application/json; charset=utf-8
 * Transfer-Encoding: chunked
 *
 */

internal class PostJsonObject : IClientRequest
{
    public async Task ExecuteAsync(HttpClient client)
    {
        await client.PostAsJsonAsync("dummyEndpoint", new DummyRequestObject());
    }

    private class DummyRequestObject
    {
        public string Property1 { get; set; } = "Value1";
    }
}
