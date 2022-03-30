using FluentValidation.Results;

namespace HappyCoding.CQSWithMediatR.Application.Infrastructure;

public class RequestValidationException : ApplicationException
{
    private IReadOnlyList<ValidationFailure> Failures { get; }

    public RequestValidationException(IReadOnlyList<ValidationFailure> failures)
        : base(failures.FirstOrDefault()?.ToString() ?? "Unknown validation error")
    {
        this.Failures = failures;
    }
}