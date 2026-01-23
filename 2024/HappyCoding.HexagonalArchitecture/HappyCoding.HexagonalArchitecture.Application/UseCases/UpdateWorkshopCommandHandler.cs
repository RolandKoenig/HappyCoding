using HappyCoding.HexagonalArchitecture.Domain.Model;
using HappyCoding.HexagonalArchitecture.Domain.Ports;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application.UseCases;

public class UpdateWorkshopCommandHandler : IRequestHandler<UpdateWorkshopCommand, Workshop>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateWorkshopCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Workshop> Handle(UpdateWorkshopCommand command, CancellationToken cancellationToken)
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