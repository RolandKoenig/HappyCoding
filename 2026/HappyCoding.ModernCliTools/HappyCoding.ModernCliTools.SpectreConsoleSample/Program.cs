using Spectre.Console;

namespace HappyCoding.ModernCliTools.SpectreConsoleSample;

public static class Program
{
    public static async Task Main(string[] args)
    {
        // // Figlet
        // var figlet = new FigletText("RolandK");
        // AnsiConsole.Write(figlet);
        
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
        //     .Spinner(Spinner.Known.Bounce)
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
        
        // // List prompt
        // var listPromptResult = AnsiConsole.Prompt<string>(
        //     new SelectionPrompt<string>()
        //         .Title("Select one of the following")
        //         .AddChoices(new[] { "Choice 1", "Choice 2", "Choice 3" }));
        // AnsiConsole.MarkupLine("You chose [green]{0}[/]", listPromptResult);
        // Console.WriteLine();
        
        // Table
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumns("Firstname", "Lastname", "Email");
        table.AddRow("Roland", "König", "roland.koenig@rolandk.de");
        AnsiConsole.Write(table);
        
        Console.ReadLine();
        Console.Clear();
    }
}