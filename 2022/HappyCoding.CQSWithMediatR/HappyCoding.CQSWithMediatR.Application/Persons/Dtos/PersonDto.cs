using FluentValidation;

namespace HappyCoding.CQSWithMediatR.Application.Persons.Dtos;

public record PersonDto
{
    public string Name { get; init; } = null!;

    public string Address { get; init; } = null!;
}