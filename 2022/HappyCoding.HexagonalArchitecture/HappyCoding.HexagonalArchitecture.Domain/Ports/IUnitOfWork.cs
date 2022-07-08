namespace HappyCoding.HexagonalArchitecture.Domain.Ports;

internal interface IUnitOfWork
{
    Task SaveChangesAsync();
}