namespace HappyCoding.ConsoleLogWindow.Application.Model;

public class ProcessInfo
{
    public string Title { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;

    public string Arguments { get; set; } = string.Empty;

    /// <summary>
    /// Optional. By default, the working directory is the same directory where FileName is located in.
    /// </summary>
    public string? WorkingDirectory { get; set; } = string.Empty;
}