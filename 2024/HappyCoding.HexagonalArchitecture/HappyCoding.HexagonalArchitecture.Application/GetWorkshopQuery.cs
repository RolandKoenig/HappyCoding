using HappyCoding.HexagonalArchitecture.Domain.Model;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public record GetWorkshopQuery(Guid ID) 
    : IRequest<Workshop>;