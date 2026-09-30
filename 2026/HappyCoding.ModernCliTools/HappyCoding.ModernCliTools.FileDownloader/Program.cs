using System.CommandLine;
using Spectre.Console;

// ReSharper disable ShortLivedHttpClient

namespace HappyCoding.ModernCliTools.FileDownloader;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        // Common options
        var verboseOption = new Option<bool>("--verbose")
        {
            Description = "Show verbose logging.",
            Recursive = true
        };
        var urlArgument = new Argument<string>("url")
        {
            Description = "The URL of the file to download."
        };
        var outputArgument = new Argument<string?>("output")
        {
            Description = "The output path for the downloaded file.",
        };

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
            await HandleCheckSizeAsync(url, verbose, cancellationToken);
        });
        rootCommand.Subcommands.Add(sizeCommand);
        
        // Define download command
        var downloadCommand = new Command("download", "Download a file from the internet.");
        downloadCommand.Arguments.Add(urlArgument);
        downloadCommand.Arguments.Add(outputArgument);
        downloadCommand.SetAction(async (parseResult, cancellationToken) =>
        {
            var url = parseResult.GetValue(urlArgument);
            var output = parseResult.GetValue(outputArgument);
            var verbose = parseResult.GetValue(verboseOption);
            
            if (url == null) return;
            await HandleDownloadAsync(url, output, verbose, cancellationToken);
        });
        rootCommand.Subcommands.Add(downloadCommand);

        // Execute the command
        try
        {
            var invocationConfig = new InvocationConfiguration();
            invocationConfig.EnableDefaultExceptionHandler = false;

            return await rootCommand.Parse(args).InvokeAsync(
                configuration: invocationConfig,
                cancellationToken: GracefulShutdownHelper.CreateCancellationToken());
        }
        catch (OperationCanceledException)
        {
            AnsiConsole.WriteLine("Canceled");
            return 1;
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"[red]Unhandled exception of type {e.GetType().FullName}: {e.Message}[/]");
            return 1;
        }
    }

    private static async Task HandleDownloadAsync(string url, string? outputPath, bool verbose, CancellationToken cancellationToken)
    {
        if (verbose)
        {
            AnsiConsole.MarkupLine("[grey]LOG: Starting download from {0}[/]", url);
        }
        
        var httpClient = new HttpClient();
        using var response =
            await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
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

                await using var contentStream = await response.Content.ReadAsStreamAsync(cancellationToken);
                await using var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write,
                    FileShare.None, 8192, true);

                var buffer = new byte[100];
                int bytesRead;
                while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                {
                    await Task.Delay(100, cancellationToken);
                    await fileStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);
                    task.Increment(bytesRead);
                }

                task.StopTask();
            });

        AnsiConsole.MarkupLine("[green]Download complete:[/] {0}", fileName);
    }

    private static async Task HandleCheckSizeAsync(string url, bool verbose, CancellationToken cancellationToken)
    {
        if (verbose)
        {
            AnsiConsole.MarkupLine("[grey]LOG: Checking size for {0}[/]", url);
        }
        
        var httpClient = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Head, url);
        using var response = await httpClient.SendAsync(request, cancellationToken);
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
}
