namespace HappyCoding.ConsoleLogWindow.Domain.Model;

public record ProcessOutputLine
{
    public DateTimeOffset Timestamp { get; init; }

    public string Text { get; init; } = string.Empty;
}
