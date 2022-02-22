using System.Threading;
using System.Threading.Tasks;
using HappyCoding.AspNetWithAurelia.Application.Books.Dtos;
using HappyCoding.AspNetWithAurelia.Application.Books.Queries;
using HappyCoding.AspNetWithAurelia.Domain.Ports;
using MediatR;

namespace HappyCoding.AspNetWithAurelia.Application.Books;

public class AuthorQueryHandler : IRequestHandler<AuthorQuery, AuthorDto>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public AuthorQueryHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public Task<AuthorDto> Handle(AuthorQuery request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}