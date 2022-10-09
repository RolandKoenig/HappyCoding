using HappyCoding.HexagonalArchitecture.Application.Dtos;
using HappyCoding.HexagonalArchitecture.Application.Util;
using HappyCoding.HexagonalArchitecture.Domain.Ports;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public class GetWorkshopRequestHandler : IRequestHandler<GetWorkshopRequest, WorkshopDto>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetWorkshopRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<WorkshopDto> Handle(GetWorkshopRequest request, CancellationToken cancellationToken)
    {
        var workshop = await _unitOfWork.Workshops.GetWorkshopAsync(request.ID, cancellationToken);
        return WorkshopMapper.WorkshopToDto(workshop);
    }
}