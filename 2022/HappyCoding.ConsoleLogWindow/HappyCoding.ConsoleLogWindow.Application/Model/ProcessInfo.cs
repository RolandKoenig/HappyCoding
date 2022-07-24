namespace HappyCoding.ConsoleLogWindow.Application.Model;

public record ProcessInfo
{
    public Guid ID { get; init; } = Guid.NewGuid();

    public string Title { get; init; } = string.Empty;

    public string CommandLine { get; init; } = string.Empty;
}