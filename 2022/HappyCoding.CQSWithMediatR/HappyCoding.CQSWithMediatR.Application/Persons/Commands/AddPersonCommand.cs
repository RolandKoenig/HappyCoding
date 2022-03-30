using HappyCoding.CQSWithMediatR.Application.Persons.Dtos;
using MediatR;

namespace HappyCoding.CQSWithMediatR.Application.Persons.Commands;

public record AddPersonCommand(PersonDto Person) : IRequest;