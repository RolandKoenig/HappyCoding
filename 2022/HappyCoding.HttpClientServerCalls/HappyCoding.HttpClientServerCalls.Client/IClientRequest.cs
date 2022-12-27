namespace HappyCoding.HttpClientServerCalls.Client;

internal interface IClientRequest
{
    Task ExecuteAsync(HttpClient client);
}
