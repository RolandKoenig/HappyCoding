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
            Duration = workshop.Duration,
            Participants = workshop.Participants.ToList(),
            Project = workshop.Project,
            Protocol = workshop.Protocol
                .Select(x => new ProtocolEntryDto()
                {
                    ChangeDate = x.ChangeDate,
                    EntryType = (ProtocolEntryTypeDto)x.EntryType,
                    Priority = x.Priority.Priority,
                    Responsible = x.Responsible
                })
                .ToList(),
            StartTimestamp = workshop.StartTimestamp,
            Title = workshop.Title
        };
    }
}