using HappyCoding.HexagonalArchitecture.Application.Dtos;
using HappyCoding.HexagonalArchitecture.Application.Util;
using HappyCoding.HexagonalArchitecture.Domain.Model;
using HappyCoding.HexagonalArchitecture.Domain.Ports;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public class CreateWorkshopRequestHandler : IRequestHandler<CreateWorkshopRequest, WorkshopDto>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateWorkshopRequestHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<WorkshopDto> Handle(CreateWorkshopRequest request, CancellationToken cancellationToken)
    {
        var workshopDto = request.Workshop;

        var newWorkshop = Workshop.CreateNew(
            workshopDto.Project,
            workshopDto.Title,
            workshopDto.StartTimestamp,
            workshopDto.Protocol.Select(x => ProtocolEntry.CreateNew(
                x.Text,
                (ProtocolEntryType)x.EntryType,
                new ProtocolEntryPriority(x.Priority))));

        await _unitOfWork.Workshops.AddWorkshopAsync(newWorkshop, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return WorkshopMapper.WorkshopToDto(newWorkshop);
    }
}