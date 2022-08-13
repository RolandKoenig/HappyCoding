using System.Collections.Immutable;
using HappyCoding.HexagonalArchitecture.Dtos;

namespace HappyCoding.HexagonalArchitecture.WebUI.Client.Services;

public interface IWorkshopClient
{
    Task<WorkshopDto> CreateWorkshopAsync(WorkshopWithoutIDDto workshop, CancellationToken cancellationToken);

    Task<WorkshopDto> UpdateWorkshopAsync(WorkshopDto workshop, CancellationToken cancellationToken);

    Task DeleteWorkshopAsync(Guid workshopID, CancellationToken cancellationToken);

    Task<ImmutableArray<WorkshopShortInfoDto>> SearchWorkshopsAsync(string queryString, CancellationToken cancellationToken);

    Task<WorkshopDto> GetWorkshopAsync(Guid workshopID, CancellationToken cancellationToken);
}
