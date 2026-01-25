using HappyCoding.HexagonalArchitecture.Application.Model;
using HappyCoding.HexagonalArchitecture.Application.Ports;

namespace HappyCoding.HexagonalArchitecture.Application.UseCases;

public class UpdateWorkshopCommandHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateWorkshopCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Workshop> HandleAsync(UpdateWorkshopCommand command, CancellationToken cancellationToken)
    {
        var workshop = await _unitOfWork.Workshops.GetWorkshopAsync(
            command.ID, cancellationToken);

        workshop.Update(
            command.Project,
            command.Title,
            command.StartTimestamp,
            command.Protocol.Select(x => ProtocolEntry.CreateNew(
                x.Text,
                (ProtocolEntryType)x.EntryType,
                x.Priority)));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return workshop;
    }
}