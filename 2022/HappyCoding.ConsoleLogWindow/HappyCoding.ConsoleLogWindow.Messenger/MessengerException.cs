namespace HappyCoding.ConsoleLogWindow.Messenger;

public class MessengerException : ApplicationException
{
    public MessengerException(string message)
        : base(message)
    {

    }

    public MessengerException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}
