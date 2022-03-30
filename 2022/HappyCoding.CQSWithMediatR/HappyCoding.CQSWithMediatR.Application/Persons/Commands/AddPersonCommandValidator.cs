using FluentValidation;
using FluentValidation.Results;
using HappyCoding.CQSWithMediatR.Application.Persons.Dtos;

namespace HappyCoding.CQSWithMediatR.Application.Persons.Commands;

internal class AddPersonCommandValidator : AbstractValidator<AddPersonCommand>
{
    public AddPersonCommandValidator(IValidator<PersonDto> personValidator)
    {
        this.RuleFor(x => x.Person).SetValidator(personValidator);
    }
}