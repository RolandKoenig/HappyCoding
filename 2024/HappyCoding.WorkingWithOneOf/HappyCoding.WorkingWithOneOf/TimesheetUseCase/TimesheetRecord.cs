using System.ComponentModel.DataAnnotations;

namespace HappyCoding.WorkingWithOneOf.TimesheetUseCase;

internal class TimesheetRecord(DateOnly date, string taskDescription, int workingHours)
{
    public DateOnly Date { get; } = date;

    public string TaskDescription { get; } = taskDescription;

    public int WorkingHours { get; } = workingHours;
}
