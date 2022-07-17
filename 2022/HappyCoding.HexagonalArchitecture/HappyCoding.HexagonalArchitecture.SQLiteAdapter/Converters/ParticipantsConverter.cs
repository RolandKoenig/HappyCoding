using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HappyCoding.HexagonalArchitecture.SQLiteAdapter.Converters;

internal class ParticipantsConverter : ValueConverter<IReadOnlyList<string>, string>
{
    public ParticipantsConverter()
        : base(appModel => ConvertCollectionToString(appModel),
            dbModel => ConvertStringToCollection(dbModel),
            null)
    {

    }

    private static string ConvertCollectionToString(IReadOnlyList<string> collection)
    {
        if (collection.Count == 0)
        {
            return string.Empty;
        }
        
        return JsonSerializer.Serialize(collection) ?? string.Empty;
    }

    private static IReadOnlyList<string> ConvertStringToCollection(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return Array.Empty<string>();
        }

        return JsonSerializer.Deserialize<string[]>(str) ?? Array.Empty<string>();
    }
}