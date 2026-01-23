using HappyCoding.HexagonalArchitecture.Application.Model;
using HappyCoding.HexagonalArchitecture.Application.Model.Projections;
using HappyCoding.HexagonalArchitecture.WebUI.Dtos;

namespace HappyCoding.HexagonalArchitecture.WebUI.Server.Api;

internal static class WorkshopMapper
{
    public static WorkshopDto WorkshopToDto(this Workshop model)
    {
        return new WorkshopDto()
        {
            ID = model.ID,
            Project = model.Project,
            Protocol = model.Protocol
                .Select(x => new ProtocolEntryDto()
                {
                    Text = x.Text,
                    EntryType = (ProtocolEntryTypeDto)x.EntryType,
                    Priority = x.Priority.Priority
                })
                .ToList(),
            StartTimestamp = model.StartTimestamp,
            Title = model.Title
        };
    }

    public static WorkshopShortInfoDto WorkshopShortInfoDto(this WorkshopShortInfo model)
    {
        return new WorkshopShortInfoDto()
        {
            ID = model.ID,
            Project = model.Project,
            StartTimestamp = model.StartTimestamp,
            Title = model.Title
        };
    }
    
    public static Workshop WorkshopDtoToModel(this WorkshopDto dto)
    {
        return Workshop.CreateNew(
            dto.Project,
            dto.Title,
            dto.StartTimestamp,
            dto.Protocol.Select(ProtocolEntryDtoToModel));
    }
    
    public static ProtocolEntry ProtocolEntryDtoToModel(this ProtocolEntryDto dto)
    {
        return ProtocolEntry.CreateNew(
            dto.Text, 
            (ProtocolEntryType)dto.EntryType, 
            new ProtocolEntryPriority(dto.Priority));
    }
}