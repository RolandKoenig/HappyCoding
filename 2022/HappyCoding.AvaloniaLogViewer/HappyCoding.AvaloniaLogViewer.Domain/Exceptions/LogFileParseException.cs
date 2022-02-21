namespace HappyCoding.AvaloniaLogViewer.Domain.Exceptions;

public class LogFileParseException : ApplicationException
{
    public LogFileParseException(string message)
        : base(message)
    {
        
    }
    
    public LogFileParseException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}