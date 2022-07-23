namespace HappyCoding.ConsoleLogWindow.Domain.Exceptions;

public class ConsoleLogWindowApplicationException : ApplicationException
{
    public ConsoleLogWindowApplicationException(string message)
        : base(message)
    {

    }

    public ConsoleLogWindowApplicationException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
