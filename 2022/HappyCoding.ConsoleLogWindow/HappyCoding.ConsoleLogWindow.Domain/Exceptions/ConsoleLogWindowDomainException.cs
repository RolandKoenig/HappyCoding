namespace HappyCoding.ConsoleLogWindow.Domain.Exceptions;

public class ConsoleLogWindowDomainException : ApplicationException
{
    public ConsoleLogWindowDomainException(string message)
        : base(message)
    {

    }

    public ConsoleLogWindowDomainException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
