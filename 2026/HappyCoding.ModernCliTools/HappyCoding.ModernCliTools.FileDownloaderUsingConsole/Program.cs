namespace HappyCoding.ModernCliTools.FileDownloaderUsingConsole;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var url = args[0];
        var outputPath = args[1];
        
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        Console.WriteLine($"File size: {response.Content.Headers.ContentLength}");
        Console.WriteLine("Downloading...");
        await using var contentStream = await response.Content.ReadAsStreamAsync();
        await using var fileStream = new FileStream(outputPath , FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);
        var buffer = new byte[100];
        int bytesRead;
        while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            await Task.Delay(100);
            Console.Write('#');
            await fileStream.WriteAsync(buffer, 0, bytesRead);
        }
        Console.WriteLine();
        Console.WriteLine();
        
        Console.WriteLine("Download complete :-)");
    }
}