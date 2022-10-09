using HappyCoding.HexagonalArchitecture.Application.Dtos;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public record GetWorkshopRequest(Guid ID) 
    : IRequest<WorkshopDto>;