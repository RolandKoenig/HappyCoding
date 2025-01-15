using HappyCoding.HexagonalArchitecture.Application.Dtos;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public record UpdateWorkshopRequest(WorkshopDto Workshop) 
    : IRequest<WorkshopDto>;