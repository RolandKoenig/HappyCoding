using System.Threading;
using System.Threading.Tasks;
using HappyCoding.AspNetWithAurelia.Application.Books.Dtos;
using HappyCoding.AspNetWithAurelia.Application.Books.Queries;
using HappyCoding.AspNetWithAurelia.Domain.Ports;
using MediatR;

namespace HappyCoding.AspNetWithAurelia.Application.Books;

public class GenreQueryHandler : IRequestHandler<GenreQuery, GenreDto>
{
    private IUnitOfWork _unitOfWork;
    
    public GenreQueryHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public Task<GenreDto> Handle(GenreQuery request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}