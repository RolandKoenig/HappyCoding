using System.Collections.Immutable;
using HappyCoding.CQSWithMediatR.Domain.Model;
using HappyCoding.CQSWithMediatR.Domain.Ports;

namespace HappyCoding.CQSWithMediatR.InMemoryRepositories;

internal class InMemoryPersonRepository : IPersonRepository
{
    private readonly List<Person> _persons;
    private readonly object _personsLock;

    public InMemoryPersonRepository()
    {
        _persons = new List<Person>();
        _personsLock = new object();
    }

    /// <inheritdoc />
    public Task<IEnumerable<Person>> SearchPersonsAsync(string? searchString)
    {
        lock (_personsLock)
        {
            var personEnumerable = (IEnumerable<Person>)_persons;

            // Apply search string (if provided)
            if (!string.IsNullOrEmpty(searchString))
            {
                personEnumerable = personEnumerable.Where(
                    x => x.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    x.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }
            
            // Return result in a separate collection
            return Task.FromResult<IEnumerable<Person>>(
                _persons.ToImmutableArray());
        }
    }

    /// <inheritdoc />
    public Task AddPersonAsync(Person person)
    {
        lock (_personsLock)
        {
            _persons.Add(person);
        }

        return Task.CompletedTask;
    }
}