using HappyCoding.HexagonalArchitecture.Application.Model;
using HappyCoding.HexagonalArchitecture.Application.Ports;

namespace HappyCoding.HexagonalArchitecture.Application.UseCases;

public class CreateWorkshopCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateWorkshopCommandHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Workshop> HandleAsync(CreateWorkshopCommand command, CancellationToken cancellationToken)
    {
        var newWorkshop = Workshop.CreateNew(
            command.Project,
            command.Title,
            command.StartTimestamp,
            command.Protocol.Select(x => ProtocolEntry.CreateNew(
                x.Text,
                (ProtocolEntryType)x.EntryType,
                x.Priority)));

        await _unitOfWork.Workshops.AddWorkshopAsync(newWorkshop, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return newWorkshop;
    }
}