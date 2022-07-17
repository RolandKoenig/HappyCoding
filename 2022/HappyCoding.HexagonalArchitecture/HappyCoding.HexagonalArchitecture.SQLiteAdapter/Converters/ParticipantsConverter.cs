using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HappyCoding.HexagonalArchitecture.SQLiteAdapter.Converters;

internal class ParticipantsConverter : ValueConverter<IReadOnlyCollection<string>, string>
{
    public ParticipantsConverter()
        : base(appModel => ConvertCollectionToString(appModel),
            dbModel => ConvertStringToCollection(dbModel),
            null)
    {

    }

    private static string ConvertCollectionToString(IReadOnlyCollection<string> collection)
    {
        if (collection.Count == 0)
        {
            return string.Empty;
        }
        
        return JsonSerializer.Serialize(collection) ?? string.Empty;
    }

    private static IReadOnlyCollection<string> ConvertStringToCollection(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return Array.Empty<string>();
        }

        return JsonSerializer.Deserialize<string[]>(str) ?? Array.Empty<string>();
    }
}