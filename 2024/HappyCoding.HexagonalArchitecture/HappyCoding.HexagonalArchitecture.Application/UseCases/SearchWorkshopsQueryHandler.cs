using HappyCoding.HexagonalArchitecture.Application.Model.Projections;
using HappyCoding.HexagonalArchitecture.Application.Ports;

namespace HappyCoding.HexagonalArchitecture.Application.UseCases;

public class SearchWorkshopsQueryHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public SearchWorkshopsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<WorkshopShortInfo>> HandleAsync(SearchWorkshopsQuery query, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Workshops.SearchWorkshopsAsync(
            query.QueryString, cancellationToken);
    }
}