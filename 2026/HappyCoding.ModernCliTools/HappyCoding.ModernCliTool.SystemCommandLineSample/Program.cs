using System.CommandLine;

namespace HappyCoding.ModernCliTool.SystemCommandLineSample;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var urlArgument = new Argument<string>("url");
        urlArgument.Description = "The URL to download from";
        
        var outputOption = new Option<string>("--output-directory", "-o");
        outputOption.Description = "The output directory";
        outputOption.DefaultValueFactory = _ => "./";

        var downloadCommand = new Command("download", "Downloads files from a given URL");
        downloadCommand.Arguments.Add(urlArgument);
        downloadCommand.Options.Add(outputOption);
        downloadCommand.SetAction(async (parseResult, cancellationToken) =>
        {
            var url = parseResult.GetValue(urlArgument);
            var outputDirectory = parseResult.GetValue(outputOption);
            
            Console.WriteLine($"Downloading from {url} to {outputDirectory}");
            await Task.Delay(1000, cancellationToken);
            Console.WriteLine("Downloaded..");
        });
        
        var checkSizeCommand = new Command("check-size", "Checks the size of a file");
        checkSizeCommand.Arguments.Add(urlArgument);
        checkSizeCommand.SetAction(async (parseResult, cancellationToken) =>
        {
            var url = parseResult.GetValue(urlArgument);
            
            Console.WriteLine($"Checking size from {url}");
            await Task.Delay(1000, cancellationToken);
            Console.WriteLine("Size checked");
        });
        
        var rootCommand = new RootCommand("Downloads files from a given URL");
        rootCommand.Subcommands.Add(downloadCommand);
        rootCommand.Subcommands.Add(checkSizeCommand);
        
        var parsedResult = rootCommand.Parse(args);
        await parsedResult.InvokeAsync();
        
        Console.ReadLine();
        Console.Clear();
    }
}