using System.ComponentModel.DataAnnotations;

namespace HappyCoding.WorkingWithOneOf.TimesheetUseCase;

internal record CreateTimesheetRecordRequest(
    DateOnly Date,
    [property: Required(AllowEmptyStrings = false)] string TaskDescription,
    [property: Range(1, 24)] int WorkingHours);