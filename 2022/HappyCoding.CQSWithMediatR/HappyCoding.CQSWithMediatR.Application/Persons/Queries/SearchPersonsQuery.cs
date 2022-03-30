using HappyCoding.CQSWithMediatR.Application.Persons.Dtos;
using MediatR;

namespace HappyCoding.CQSWithMediatR.Application.Persons.Queries;

public record SearchPersonsQuery : IRequest<IEnumerable<PersonDto>>
{
    public string? SearchString { get; init; }
}