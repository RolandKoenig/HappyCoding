namespace HappyCoding.ConsoleLogWindow.Messenger;

public class MessageAsyncRoutingTargetsAttribute : Attribute
{
    public string[] AsyncTargetMessengers { get; }

    public MessageAsyncRoutingTargetsAttribute(params string[] asyncTargetMessengers)
    {
        this.AsyncTargetMessengers = asyncTargetMessengers;
    }
}