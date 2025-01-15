namespace HappyCoding.HexagonalArchitecture.Domain;

public class DomainException : ApplicationException
{
    public DomainException(string message)
        : base(message)
    {
        
    }

    public DomainException(string message, Exception ex)
        : base(message, ex)
    {
        
    }
}