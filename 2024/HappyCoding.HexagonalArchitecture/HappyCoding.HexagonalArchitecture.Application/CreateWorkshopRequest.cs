using HappyCoding.HexagonalArchitecture.Application.Dtos;
using MediatR;

namespace HappyCoding.HexagonalArchitecture.Application;

public record CreateWorkshopRequest(WorkshopWithoutIDDto Workshop) 
    : IRequest<WorkshopDto>;