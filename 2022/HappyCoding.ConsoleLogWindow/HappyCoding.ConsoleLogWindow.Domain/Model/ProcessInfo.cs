namespace HappyCoding.ConsoleLogWindow.Domain.Model;

public class ProcessInfo
{
    public string Title { get; private set; }

    public string CommandLine { get; private set; }

    public ProcessInfo(string title)
    {
        this.Title = title;
        this.CommandLine = "";
    }
}