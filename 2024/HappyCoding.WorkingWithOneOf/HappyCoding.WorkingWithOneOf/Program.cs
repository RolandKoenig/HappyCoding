using HappyCoding.WorkingWithOneOf.TimesheetUseCase;

namespace HappyCoding.WorkingWithOneOf;

internal class Program
{
    internal static async Task Main()
    {
        // Case 1 (TimesheetUseCase)
        Console.WriteLine("## Case 1 - TimesheetUseCase");
        var request = new CreateTimesheetRecordRequest(new DateOnly(2021, 10, 1), "Task 1", 8);
        var handler = new CreateTimesheetRecordHandler();
        var result = await handler.CreateTimesheetRecordAsync(request);
        result.Switch(
            _ => Console.WriteLine("New timesheet record created"),
            validationError => Console.WriteLine($"Validation error: {string.Join(", ", validationError.ValidationErrors.Select(e => e.ErrorMessage))}"),
            recordAlreadyExistsError => Console.WriteLine("Record already exists"));
        Console.WriteLine();

        // Case 1 (TimesheetUseCase)
        Console.WriteLine("## Case 2 - TimesheetUseCase, Validation error");
        request = new CreateTimesheetRecordRequest(new DateOnly(2021, 10, 1), "", 8);
        handler = new CreateTimesheetRecordHandler();
        result = await handler.CreateTimesheetRecordAsync(request);
        result.Switch(
            _ => Console.WriteLine("New timesheet record created"),
            validationError => Console.WriteLine($"Validation error: {string.Join(", ", validationError.ValidationErrors.Select(e => e.ErrorMessage))}"),
            recordAlreadyExistsError => Console.WriteLine("Record already exists"));
        Console.WriteLine();
    }
}
