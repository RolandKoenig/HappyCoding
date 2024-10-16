using System.Text;

namespace HappyCoding.RequestCompression.Client;

public static class Program
{
    private const string TARGET_BASE_ADDRESS = "http://localhost:5000";
    private const string TARGET_ENDPOINT = "/hello";
    private const string BODY_UNCOMPRESSED = "Hello Service!";
    
    public static async Task Main(string[] args)
    {
        bool doContinue;
        do
        {
            doContinue = await TriggerSingleCallAsync();
        } while (doContinue);
    }

    private static async Task<bool> TriggerSingleCallAsync()
    {
        Console.WriteLine("Select which request to trigger:");
        Console.WriteLine(" 1 = Uncompressed");
        Console.WriteLine(" 2 = Compressed by gzip");
        Console.WriteLine();

        Console.Write("Input: ");
        var selection = Console.ReadLine();
        if (string.IsNullOrEmpty(selection)) { return false; }
        
        switch (selection.ToLower())
        {
            case "1":
                await TriggerRequestUncompressedAsync();
                Console.WriteLine("Request triggered");
                break;
            
            case "2":
                await TriggerRequestCompressedByGzipAsync();
                Console.WriteLine("Request triggered");
                break;
            
            default:
                Console.WriteLine("Invalid input");
                break;
        }

        Console.WriteLine();
        return true;
    }

    private static async Task TriggerRequestUncompressedAsync()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(TARGET_BASE_ADDRESS);

        using var httpContent = new StringContent(BODY_UNCOMPRESSED, Encoding.UTF8);
        await httpClient.PostAsync(TARGET_ENDPOINT, httpContent);
    }

    private static async Task TriggerRequestCompressedByGzipAsync()
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(TARGET_BASE_ADDRESS);
        
        using var httpContent = new StringContent(BODY_UNCOMPRESSED, Encoding.UTF8);
        using var compressedContent = new CompressedContent(httpContent, "gzip");
        
        await httpClient.PostAsync(TARGET_ENDPOINT, compressedContent);
    }
}
