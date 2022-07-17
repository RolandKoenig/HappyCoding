using HappyCoding.HexagonalArchitecture.Domain.Model;

namespace HappyCoding.HexagonalArchitecture.Domain.Ports;

public interface IWorkshopRepository
{
    Task AddWorkshopAsync(Workshop workshop, CancellationToken cancellationToken);

    Task DeleteWorkshopAsync(Workshop workshop, CancellationToken cancellationToken);
}