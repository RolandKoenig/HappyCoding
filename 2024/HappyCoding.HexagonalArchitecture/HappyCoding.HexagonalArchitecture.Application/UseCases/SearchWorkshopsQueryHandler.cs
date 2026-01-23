using HappyCoding.HexagonalArchitecture.Application.Model.Projections;
using HappyCoding.HexagonalArchitecture.Application.Ports;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application.UseCases;

public class SearchWorkshopsQueryHandler : IRequestHandler<SearchWorkshopsQuery, IEnumerable<WorkshopShortInfo>>
{
    private readonly IUnitOfWork _unitOfWork;

    public SearchWorkshopsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<WorkshopShortInfo>> Handle(SearchWorkshopsQuery query, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Workshops.SearchWorkshopsAsync(
            query.QueryString, cancellationToken);
    }
}