using System.Collections.Immutable;
using HappyCoding.HexagonalArchitecture.Application.Model;
using HappyCoding.HexagonalArchitecture.Application.Model.Projections;

namespace HappyCoding.HexagonalArchitecture.Application.Ports;

public interface IWorkshopRepository
{
    Task AddWorkshopAsync(Workshop workshop, CancellationToken cancellationToken);

    Task<Workshop> GetWorkshopAsync(Guid workshopID, CancellationToken cancellationToken);

    Task<Workshop?> TryGetWorkshopAsync(Guid workshopID, CancellationToken cancellationToken);

    Task<ImmutableArray<WorkshopShortInfo>> SearchWorkshopsAsync(string queryString, CancellationToken cancellationToken);

    Task DeleteWorkshopAsync(Guid workshopID, CancellationToken cancellationToken);
}