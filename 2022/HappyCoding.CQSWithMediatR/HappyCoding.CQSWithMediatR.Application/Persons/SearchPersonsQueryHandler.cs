using System.Collections.Immutable;
using HappyCoding.CQSWithMediatR.Application.Persons.Dtos;
using HappyCoding.CQSWithMediatR.Application.Persons.Queries;
using HappyCoding.CQSWithMediatR.Domain.Ports;
using MediatR;

namespace HappyCoding.CQSWithMediatR.Application.Persons;

internal class SearchPersonsQueryHandler : IRequestHandler<SearchPersonsQuery, IEnumerable<PersonDto>>
{
    private readonly IPersonRepository _repoPersons;

    public SearchPersonsQueryHandler(IPersonRepository repoPersons)
    {
        _repoPersons = repoPersons;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<PersonDto>> Handle(SearchPersonsQuery request, CancellationToken cancellationToken)
    {
        var persons = await _repoPersons.SearchPersonsAsync(request.SearchString);

        return persons
            .Select(x => new PersonDto()
            {
                Name = x.Name,
                Address = x.Address
            })
            .ToImmutableArray();
    }
}