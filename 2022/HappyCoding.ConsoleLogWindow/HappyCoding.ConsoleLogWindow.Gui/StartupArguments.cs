using CommandLine;

namespace HappyCoding.ConsoleLogWindow.Gui;

public class StartupArguments
{
    [Value(0, Required = false)]
    public string? InitialFile { get; set; }

    public static StartupArguments ParseStartupArguments(string[] args)
    {
        var parser = new Parser(settings =>
        {
            settings.IgnoreUnknownArguments = true;
        });

        var parsedArguments = parser.ParseArguments<StartupArguments>(args);
        return parsedArguments.Value ?? new StartupArguments();
    }
}
