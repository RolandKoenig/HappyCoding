using System.Runtime.Serialization;

namespace HappyCoding.ControlledProcessShutdownCall;

[Serializable]
internal class UnableToEndProcessException : ApplicationException
{
    public UnableToEndProcessException(string message)
        : base(message)
    {

    }

    protected UnableToEndProcessException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {

    }
}
