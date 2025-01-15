using HappyCoding.HexagonalArchitecture.Application.Dtos;
using HappyCoding.HexagonalArchitecture.Domain.Model;

namespace HappyCoding.HexagonalArchitecture.Application.Util;

internal static class WorkshopMapper
{
    public static WorkshopDto WorkshopToDto(Workshop workshop)
    {
        return new WorkshopDto()
        {
            ID = workshop.ID,
            Project = workshop.Project,
            Protocol = workshop.Protocol
                .Select(x => new ProtocolEntryDto()
                {
                    Text = x.Text,
                    EntryType = (ProtocolEntryTypeDto)x.EntryType,
                    Priority = x.Priority.Priority
                })
                .ToList(),
            StartTimestamp = workshop.StartTimestamp,
            Title = workshop.Title
        };
    }
}