using HappyCoding.HexagonalArchitecture.Application.Dtos;
using HappyCoding.HexagonalArchitecture.Application.Util;
using HappyCoding.HexagonalArchitecture.Domain.Model;
using HappyCoding.HexagonalArchitecture.Domain.Ports;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public class UpdateWorkshopRequestHandler : IRequestHandler<UpdateWorkshopRequest, WorkshopDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateWorkshopRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<WorkshopDto> Handle(UpdateWorkshopRequest request, CancellationToken cancellationToken)
    {
        var workshopDto = request.Workshop;
        
        var workshop = await _unitOfWork.Workshops.GetWorkshopAsync(
            request.Workshop.ID, cancellationToken);

        workshop.Update(
            workshopDto.Project,
            workshopDto.Title,
            workshopDto.StartTimestamp,
            workshopDto.Protocol.Select(x => ProtocolEntry.CreateNew(
                x.Text,
                (ProtocolEntryType)x.EntryType,
                new ProtocolEntryPriority(x.Priority))));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return WorkshopMapper.WorkshopToDto(workshop);
    }
}