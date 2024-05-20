using OneOf;
using OneOf.Types;

namespace HappyCoding.WorkingWithOneOf.TimesheetUseCase;

internal class TimesheetRepository
{
    public async Task<OneOf<Success, TimesheetUseCaseErrors.RecordAlreadyExistsError>> AddNewTimesheetRecordAsync(TimesheetRecord record)
    {
        await Task.Delay(100);

        return new Success();
    }
}
