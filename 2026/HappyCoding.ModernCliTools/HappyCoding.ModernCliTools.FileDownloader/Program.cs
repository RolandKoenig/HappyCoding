using System.CommandLine;
using Spectre.Console;

namespace HappyCoding.ModernCliTools.FileDownloader;

internal static class Program
{
    private static readonly HttpClient s_httpClient = new();
    
    static async Task<int> Main(string[] args)
    {
        // Common options
        var verboseOption = new Option<bool>("--verbose")
        {
            Description = "Show verbose logging.",
            Recursive = true
        };
        var urlArgument = new Argument<string>("url") { Description = "The URL of the file to download." };
        var outputOption = new Option<string?>("--output") { Description = "The output path for the downloaded file." };

        // Root command
        var rootCommand = new RootCommand("A small CLI tool to download files or check their size.");
        rootCommand.Options.Add(verboseOption);

        // Define check-size command
        var sizeCommand = new Command("check-size", "Check the size of a file on the internet.");
        sizeCommand.Arguments.Add(urlArgument);
        sizeCommand.SetAction(async (parseResult, cancellationToken) =>
        {
            var url = parseResult.GetValue(urlArgument);
            var verbose = parseResult.GetValue(verboseOption);
            
            if (url == null) return;
            await HandleCheckSizeAsync(url, verbose);
        });
        rootCommand.Subcommands.Add(sizeCommand);
        
        // Define download command
        var downloadCommand = new Command("download", "Download a file from the internet.");
        downloadCommand.Arguments.Add(urlArgument);
        downloadCommand.Options.Add(outputOption);
        downloadCommand.SetAction(async (parseResult, cancellationToken) =>
        {
            var url = parseResult.GetValue(urlArgument);
            var output = parseResult.GetValue(outputOption);
            var verbose = parseResult.GetValue(verboseOption);
            
            if (url == null) return;
            await HandleDownloadAsync(url, output, verbose);
        });
        rootCommand.Subcommands.Add(downloadCommand);

        // Execute the command
        return await rootCommand.Parse(args).InvokeAsync();
    }

    private static async Task HandleDownloadAsync(string url, string? outputPath, bool verbose)
    {
        if (verbose)
        {
            AnsiConsole.MarkupLine("[grey]LOG: Starting download from {0}[/]", url);
        }
        
        try
        {
            using var response = await s_httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            var totalBytes = response.Content.Headers.ContentLength;
            var fileName = outputPath ?? Path.GetFileName(new Uri(url).LocalPath);
            if (string.IsNullOrWhiteSpace(fileName)) fileName = "downloaded_file";

            if (verbose)
            {
                AnsiConsole.MarkupLine("[grey]LOG: Saving to {0}[/]", fileName);
            }

            await AnsiConsole.Progress()
                .StartAsync(async ctx =>
                {
                    var task = ctx.AddTask("[green]Downloading[/]", autoStart: false);
                    if (totalBytes.HasValue)
                    {
                        task.MaxValue = totalBytes.Value;
                    }
                    task.StartTask();

                    using var contentStream = await response.Content.ReadAsStreamAsync();
                    using var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true);

                    var buffer = new byte[100];
                    int bytesRead;
                    while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await Task.Delay(100);
                        await fileStream.WriteAsync(buffer, 0, bytesRead);
                        task.Increment(bytesRead);
                    }
                    
                    task.StopTask();
                });

            AnsiConsole.MarkupLine("[green]Download complete:[/] {0}", fileName);
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex);
        }
    }

    private static async Task HandleCheckSizeAsync(string url, bool verbose)
    {
        if (verbose)
        {
            AnsiConsole.MarkupLine("[grey]LOG: Checking size for {0}[/]", url);
        }

        try
        {
            var request = new HttpRequestMessage(HttpMethod.Head, url);
            using var response = await s_httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var totalBytes = response.Content.Headers.ContentLength;
            if (totalBytes.HasValue)
            {
                AnsiConsole.MarkupLine("File size: [yellow]{0}[/] bytes ({1:N2} MB)", 
                    totalBytes.Value, 
                    totalBytes.Value / 1024.0 / 1024.0);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Could not determine file size.[/]");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
    }
}
