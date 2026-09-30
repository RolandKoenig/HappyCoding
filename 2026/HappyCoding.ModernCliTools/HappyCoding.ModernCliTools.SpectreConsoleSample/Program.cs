using Spectre.Console;

namespace HappyCoding.ModernCliTools.SpectreConsoleSample;

public static class Program
{
    public static async Task Main(string[] args)
    {
        // Figlet
        var figlet = new FigletText("RolandK");
        AnsiConsole.Write(figlet);
        
        // // List prompt
        // var listPromptResult = AnsiConsole.Prompt<string>(
        //     new SelectionPrompt<string>()
        //         .Title("Select one of the following")
        //         .AddChoices(new[] { "Choice 1", "Choice 2", "Choice 3" }));
        // AnsiConsole.MarkupLine("You chose {0}", listPromptResult);
        // Console.WriteLine();
        
        // // Prompt for a string
        // var location = await AnsiConsole.AskAsync<string>("Where are you?");
        // AnsiConsole.MarkupLineInterpolated($"Your are at {location}");
        
        // // Ask for confirmation
        // var result = await AnsiConsole.ConfirmAsync("Continue?");
        // AnsiConsole.MarkupLineInterpolated($"Result: {result}");
        
        // // Formatted text
        // AnsiConsole.MarkupLine("[green bold]This is important text[/] this not"); 
        // Console.WriteLine();
        
        // // Interpolate user input
        // var userInput = "Dummy [green]test[/]";
        // AnsiConsole.MarkupLineInterpolated($"[green]User input:[/] {userInput}");
        // Console.WriteLine();
        
        // // Emoji
        // AnsiConsole.WriteLine($"{Emoji.Known.ClinkingBeerMugs} Hello");
        // AnsiConsole.Markup($":clinking_beer_mugs: Hello");
        // Console.WriteLine();
        
        // // Status
        // await AnsiConsole.Status()
        //     // .Spinner(Spinner.Known.Bounce)
        //     .StartAsync("Initializing...", async ctx =>
        //     {
        //         await Task.Delay(1000);
        //         ctx.Status("Loading configuration...");
        //         await Task.Delay(1000);
        //         ctx.Status("Starting services...");
        //         await Task.Delay(1000);
        //     });
        
        // // Progress bar
        // await AnsiConsole.Progress()
        //     .Columns(
        //         new TaskDescriptionColumn(),
        //         new ProgressBarColumn(),
        //         new SpinnerColumn(Spinner.Known.Dots))
        //     .StartAsync(async ctx =>
        //     {
        //         var progressTask1 = ctx.AddTask("Doing something");
        //         var progressTask2 = ctx.AddTask("Doing something else");
        //         
        //         await Task.Delay(2000);
        //         progressTask1.StopTask();
        //         
        //         await Task.Delay(1000);
        //         progressTask2.StopTask();
        //     });
        
        // // Table
        // var table = new Table();
        // table.Border(TableBorder.Rounded);
        // table.AddColumns("Firstname", "Lastname", "Email");
        // table.AddRow("Roland", "König", "roland.koenig@rolandk.de");
        // AnsiConsole.Write(table);
        
        // // Panel
        // // var figlet = new FigletText("RolandK");
        // var panel = new Panel("Test");
        // AnsiConsole.Write(panel);
        
        // // Tree
        // var tree = new Tree("Project Files");
        // var src = tree.AddNode("src");
        // src.AddNode("[green]Program.cs[/]");
        // src.AddNode("[green]Config.cs[/]");
        // var docs = tree.AddNode("docs");
        // docs.AddNode("README.md");
        // docs.AddNode("API.md");
        // AnsiConsole.Write(tree);
        
        Console.ReadLine();
        Console.Clear();
    }
}