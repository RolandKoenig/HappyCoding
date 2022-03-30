using HappyCoding.CQSWithMediatR.Application.Persons.Commands;
using HappyCoding.CQSWithMediatR.Domain.Model;
using HappyCoding.CQSWithMediatR.Domain.Ports;
using MediatR;

namespace HappyCoding.CQSWithMediatR.Application.Persons;

internal class AddPersonCommandHandler : IRequestHandler<AddPersonCommand>
{
    private readonly IPersonRepository _repoPersons;

    public AddPersonCommandHandler(IPersonRepository repoPersons)
    {
        _repoPersons = repoPersons;
    }

    /// <inheritdoc />
    public async Task<Unit> Handle(AddPersonCommand request, CancellationToken cancellationToken)
    {
        await _repoPersons.AddPersonAsync(new Person(
            request.Person.Name,
            request.Person.Address));

        return Unit.Value;
    }
}