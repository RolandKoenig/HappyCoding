using HappyCoding.HexagonalArchitecture.Domain.Ports;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public class DeleteWorkshopCommandHandler : IRequestHandler<DeleteWorkshopCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteWorkshopCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(DeleteWorkshopCommand command, CancellationToken cancellationToken)
    {
        await _unitOfWork.Workshops.DeleteWorkshopAsync(command.WorkshopID, cancellationToken);
 
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}