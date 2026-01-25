using HappyCoding.HexagonalArchitecture.Application.Ports;

namespace HappyCoding.HexagonalArchitecture.Application.UseCases;

public class DeleteWorkshopCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteWorkshopCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task HandleAsync(DeleteWorkshopCommand command, CancellationToken cancellationToken)
    {
        await _unitOfWork.Workshops.DeleteWorkshopAsync(command.WorkshopID, cancellationToken);
 
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}