using HappyCoding.HexagonalArchitecture.Application.Model;
using HappyCoding.HexagonalArchitecture.Application.Ports;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application.UseCases;

public class GetWorkshopQueryHandler : IRequestHandler<GetWorkshopQuery, Workshop>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetWorkshopQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Workshop> Handle(GetWorkshopQuery query, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Workshops.GetWorkshopAsync(query.ID, cancellationToken);
    }
}