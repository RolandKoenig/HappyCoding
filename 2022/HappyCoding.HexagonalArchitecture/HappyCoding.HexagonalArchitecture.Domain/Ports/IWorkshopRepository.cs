using System.Collections.Immutable;
using HappyCoding.HexagonalArchitecture.Domain.Model;
using HappyCoding.HexagonalArchitecture.Domain.Model.Projections;

namespace HappyCoding.HexagonalArchitecture.Domain.Ports;

public interface IWorkshopRepository
{
    Task AddWorkshopAsync(Workshop workshop, CancellationToken cancellationToken);

    Task<Workshop> GetWorkshopAsync(Guid workshopID, CancellationToken cancellationToken);

    Task<Workshop?> TryGetWorkshopAsync(Guid workshopID, CancellationToken cancellationToken);

    Task<ImmutableArray<WorkshopShortInfo>> SearchWorkshopsAsync(string queryString, CancellationToken cancellationToken);

    Task DeleteWorkshopAsync(Guid workshopID, CancellationToken cancellationToken);
}