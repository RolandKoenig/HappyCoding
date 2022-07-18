using HappyCoding.HexagonalArchitecture.Domain.Ports;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public class DeleteWorkshopRequestHandler : IRequestHandler<DeleteWorkshopRequest>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteWorkshopRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Unit> Handle(DeleteWorkshopRequest request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Workshops.DeleteWorkshopAsync(request.WorkshopID, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}