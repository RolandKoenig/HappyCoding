using System.Net.Http.Headers;

namespace HappyCoding.HttpClientServerCalls.Client.Requests;

/* Produces http headers:
 *
 * Host: localhost:5001
 * Content-Type: multipart/form-data; boundary="1299be49-7d4f-4ea0-b6b8-aeea9a4669e3"
 * Content-Length: 629
 *
 */

internal class UploadFile : IClientRequest
{
    public async Task ExecuteAsync(HttpClient client)
    {
        using var multipartFormContent = new MultipartFormDataContent();

        //Load the file and set the file's Content-Type header
        var fileStreamContent = new StreamContent(File.OpenRead("DummyImage.png"));
        fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");

        //Add the file
        multipartFormContent.Add(fileStreamContent, name: "file", fileName: "house.png");

        //Send it
        await client.PostAsync("dummyEndpoint", multipartFormContent);
    }
}
