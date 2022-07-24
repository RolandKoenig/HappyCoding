namespace HappyCoding.ConsoleLogWindow.Application.Exceptions;

public class ConsoleLogWindowAdapterException : ApplicationException
{
    public string AdapterName { get; }

    public ConsoleLogWindowAdapterException(string adapterName, string message)
        : base(message)
    {
        this.AdapterName = adapterName;
    }

    public ConsoleLogWindowAdapterException(string adapterName, string message, Exception innerException)
        : base(message, innerException)
    {
        this.AdapterName = adapterName;
    }
}
