namespace RolandK.Checking;

public class CommonLibCheckException : CommonLibException
{
    /// <summary>
    /// Creates a new CommonLibraryException object
    /// </summary>
    public CommonLibCheckException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Creates a new CommonLibraryException object
    /// </summary>
    public CommonLibCheckException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}