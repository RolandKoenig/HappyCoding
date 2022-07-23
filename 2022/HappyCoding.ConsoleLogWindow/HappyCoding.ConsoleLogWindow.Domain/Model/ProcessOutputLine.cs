namespace HappyCoding.ConsoleLogWindow.Domain.Model;

public class ProcessOutputLine
{
    public DateTimeOffset Timestamp { get; private set; }

    public string Text { get; private set; }

    public ProcessOutputLine(DateTimeOffset timestamp, string text)
    {
        this.Timestamp = timestamp;
        this.Text = text;
    }
}
