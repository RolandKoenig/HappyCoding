using System.Threading.Tasks;
using HappyCoding.AspNetWithAurelia.Domain.Ports.Repositories;

namespace HappyCoding.AspNetWithAurelia.Domain.Ports;

public interface IUnitOfWork
{
    IAuthorRepository Authors { get; }
    
    IBookRepository Books { get; }
    
    IGenreRepository Genres { get; }

    Task SaveChangesAsync();
}