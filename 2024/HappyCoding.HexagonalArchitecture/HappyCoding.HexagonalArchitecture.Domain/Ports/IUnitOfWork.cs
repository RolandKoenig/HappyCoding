namespace HappyCoding.HexagonalArchitecture.Domain.Ports;

public interface IUnitOfWork
{
    IWorkshopRepository Workshops { get; }
    
    Task SaveChangesAsync(CancellationToken cancellationToken);
}