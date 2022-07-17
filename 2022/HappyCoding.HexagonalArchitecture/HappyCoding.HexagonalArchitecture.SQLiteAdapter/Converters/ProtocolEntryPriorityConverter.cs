using HappyCoding.HexagonalArchitecture.Domain.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HappyCoding.HexagonalArchitecture.SQLiteAdapter.Converters;

internal class ProtocolEntryPriorityConverter : ValueConverter<ProtocolEntryPriority, int>
{
    public ProtocolEntryPriorityConverter() : base(
        x => x.Priority,
        x => new ProtocolEntryPriority(x),
        null)
    {
        
    }
}