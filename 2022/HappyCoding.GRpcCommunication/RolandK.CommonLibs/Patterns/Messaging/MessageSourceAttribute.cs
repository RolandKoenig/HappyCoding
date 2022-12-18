namespace RolandK.Patterns.Messaging;

public class MessagePossibleSourceAttribute : Attribute
{
    public string[] PossibleSourceMessengers { get; }

    public MessagePossibleSourceAttribute(params string[] possibleSourceMessengers)
    {
        this.PossibleSourceMessengers = possibleSourceMessengers;
    }
}