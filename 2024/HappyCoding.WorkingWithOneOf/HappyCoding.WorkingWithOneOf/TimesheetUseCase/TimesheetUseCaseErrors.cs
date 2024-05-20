using System.ComponentModel.DataAnnotations;

namespace HappyCoding.WorkingWithOneOf.TimesheetUseCase;

internal static class TimesheetUseCaseErrors
{
    public record struct ValidationError(IReadOnlyList<ValidationResult> ValidationErrors);

    public record struct RecordAlreadyExistsError();
}
