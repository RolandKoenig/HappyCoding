using HappyCoding.HttpClientServerCalls.Client.Requests;

namespace HappyCoding.HttpClientServerCalls.Client;

internal class Program
{
    private static HttpClient s_httpClient;

    static async Task Main()
    {
        s_httpClient = new HttpClient();
        s_httpClient.BaseAddress = new Uri("http://localhost:5001");

        var requests = new IClientRequest[]
        {
            new Get(),
            new PostJsonObject(),
            new PutJsonObject(),
            new Delete(),
            new UploadFile()
        };

        while (true)
        {
            // Write possible request to console
            Console.WriteLine("Possible actions (empty means exit): ");
            for (var loop = 0; loop < requests.Length; loop++)
            {
                Console.WriteLine($" {loop + 1} = {requests[loop].GetType().Name}");
            }
            Console.WriteLine();

            // Get user's input
            Console.Write("Next action: ");
            var nextAction = Console.ReadLine();
            if (string.IsNullOrEmpty(nextAction))
            {
                break;
            }

            // Parsing and validation
            if (!uint.TryParse(nextAction, out var nextRequestId))
            {
                Console.WriteLine("Unable to parse input");
                Console.WriteLine();
                continue;
            }
            if ((nextRequestId > requests.Length) || (nextRequestId < 1))
            {
                Console.WriteLine("Unrecognized input");
                Console.WriteLine();
                continue;
            }

            // Process request
            var nextRequest = requests[nextRequestId - 1];
            try
            {
                await nextRequest.ExecuteAsync(s_httpClient);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine();
        }
    }
}
