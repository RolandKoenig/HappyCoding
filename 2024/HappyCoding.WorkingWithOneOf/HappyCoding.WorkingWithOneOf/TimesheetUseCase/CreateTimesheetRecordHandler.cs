using System.ComponentModel.DataAnnotations;

namespace HappyCoding.WorkingWithOneOf.TimesheetUseCase;

using HandlerResult = OneOf.OneOf<
    TimesheetRecord,
    TimesheetUseCaseErrors.ValidationError,
    TimesheetUseCaseErrors.RecordAlreadyExistsError>;

internal class CreateTimesheetRecordHandler
{
    private TimesheetRepository _repository = new TimesheetRepository();

    public async Task<HandlerResult> CreateTimesheetRecordAsync(CreateTimesheetRecordRequest request)
    {
        // Validation
        var validationResults = new List<ValidationResult>();
        if(!Validator.TryValidateObject(request, new ValidationContext(request), validationResults, true))
        {
            return new TimesheetUseCaseErrors.ValidationError(validationResults);
        }

        // Business logic
        var record = new TimesheetRecord(request.Date, request.TaskDescription, request.WorkingHours);
        var addResult = await _repository.AddNewTimesheetRecordAsync(record);

        return addResult.Match<HandlerResult>(
            success => record,
            recordAlreadyExistsError => recordAlreadyExistsError);
    }
}
