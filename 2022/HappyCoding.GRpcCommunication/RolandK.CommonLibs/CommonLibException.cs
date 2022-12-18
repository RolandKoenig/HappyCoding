using System.Runtime.Serialization;

namespace RolandK;

[Serializable]
public class CommonLibException : Exception
{
    public CommonLibException()
        : base()
    {

    }

    public CommonLibException(string message)
        : base(message)
    {

    }

    public CommonLibException(string message, Exception innerException)
        : base(message, innerException)
    {

    }

    protected CommonLibException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {

    }
}
