namespace HappyCoding.AvaloniaLogViewer.Domain.Model;

public class LogFileEntry
{
    public DateTimeOffset Timestamp { get; set; }
    
    public LogLevel LogLevel { get; set; }
    
    public string? Message { get; set; }
    
    public IDictionary<string, string>? MetaData { get; set; }
}