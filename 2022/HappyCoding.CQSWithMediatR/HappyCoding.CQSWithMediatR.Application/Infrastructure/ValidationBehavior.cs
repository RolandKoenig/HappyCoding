using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace HappyCoding.CQSWithMediatR.Application.Infrastructure;

// Automatic validation for MediatR pipeline
// see https://timdows.com/projects/use-mediatr-with-fluentvalidation-in-the-asp-net-core-pipeline/

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (!_validators.Any()) { return await next(); }

        var validationContext = new ValidationContext<TRequest>(request);
        var failures = (List<ValidationFailure>?) null;
        foreach (var actValidator in _validators)
        {
            var actResult = await actValidator.ValidateAsync(validationContext, cancellationToken);
            if (!actResult.IsValid)
            {
                failures ??= new List<ValidationFailure>(actResult.Errors.Count);
                failures.AddRange(actResult.Errors);
            }
        }

        if (failures is {Count: > 0})
        {
            throw new RequestValidationException(failures);
        }

        return await next();
    }
}