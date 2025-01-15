using HappyCoding.HexagonalArchitecture.Application.Dtos;
using HappyCoding.HexagonalArchitecture.Domain.Ports;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public class SearchWorkshopsRequestHandler : IRequestHandler<SearchWorkshopsRequest, IEnumerable<WorkshopShortInfoDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public SearchWorkshopsRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<WorkshopShortInfoDto>> Handle(SearchWorkshopsRequest request, CancellationToken cancellationToken)
    {
        var searchResults = await _unitOfWork.Workshops.SearchWorkshopsAsync(
            request.QueryString, cancellationToken);

        return searchResults.Select(x => new WorkshopShortInfoDto()
        {
            ID = x.ID,
            Project = x.Project,
            StartTimestamp = x.StartTimestamp,
            Title = x.Title
        });
    }
}