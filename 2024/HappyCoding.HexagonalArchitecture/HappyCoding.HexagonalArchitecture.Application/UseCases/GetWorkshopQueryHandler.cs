using HappyCoding.HexagonalArchitecture.Application.Model;
using HappyCoding.HexagonalArchitecture.Application.Ports;

namespace HappyCoding.HexagonalArchitecture.Application.UseCases;

public class GetWorkshopQueryHandler
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetWorkshopQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Workshop> HandleAsync(GetWorkshopQuery query, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Workshops.GetWorkshopAsync(query.ID, cancellationToken);
    }
}