using HappyCoding.CQSWithMediatR.Domain.Model;

namespace HappyCoding.CQSWithMediatR.Domain.Ports;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> SearchPersonsAsync(string? searchString);

    Task AddPersonAsync(Person person);
}