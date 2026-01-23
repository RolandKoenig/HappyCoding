using HappyCoding.HexagonalArchitecture.Domain.Model;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application.UseCases;

public record GetWorkshopQuery(Guid ID) 
    : IRequest<Workshop>;