using FluentValidation;

namespace HappyCoding.CQSWithMediatR.Application.Persons.Dtos;

internal class PersonDtoValidator : AbstractValidator<PersonDto>
{
    public PersonDtoValidator()
    {
        this.RuleFor(x => x.Name).NotNull().NotEmpty();
        this.RuleFor(x => x.Address).NotNull().NotEmpty();
    }
}