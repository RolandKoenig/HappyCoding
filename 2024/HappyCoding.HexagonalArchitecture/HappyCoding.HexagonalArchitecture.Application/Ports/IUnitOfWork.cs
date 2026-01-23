namespace HappyCoding.HexagonalArchitecture.Application.Ports;

public interface IUnitOfWork
{
    IWorkshopRepository Workshops { get; }
    
    Task SaveChangesAsync(CancellationToken cancellationToken);
}