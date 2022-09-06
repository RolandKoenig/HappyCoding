using System.Runtime.Serialization;

namespace HappyCoding.ControlledProcessShutdownCall;

[Serializable]
internal class UnableToStopProcessException : ApplicationException
{
    public UnableToStopProcessException(string message)
        : base(message)
    {

    }

    protected UnableToStopProcessException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {

    }
}
