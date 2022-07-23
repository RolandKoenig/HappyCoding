namespace HappyCoding.ConsoleLogWindow.Domain.Model;

public class ProcessInfo
{
    public string Title { get; set; }

    public string CommandLine { get; set; }

    public ProcessInfo(string title)
    {
        this.Title = title;
        this.CommandLine = "";
    }
}