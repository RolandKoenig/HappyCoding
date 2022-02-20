using System.Globalization;
using HappyCoding.AvaloniaLogViewer.Domain;
using HappyCoding.AvaloniaLogViewer.Domain.Model;
using Newtonsoft.Json;

namespace HappyCoding.AvaloniaLogViewer.FileLoaders;

public class CompactJsonFileLoader : ILogFileLoader
{
    private static readonly string[] FILE_EXTENSIONS = { "clef" };
    
    public string LoaderName => "Compact Json";

    public IEnumerable<string> FileExtensions => FILE_EXTENSIONS;
    
    public async Task<IAsyncEnumerable<LogFileEntry>> LoadFileAsync(string inputFile, CancellationToken cancellationToken)
    {
        await using var inStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var inStreamReader = new StreamReader(inStream);

        var actLine = await inStreamReader.ReadLineAsync();
        while ((actLine != null) &&
               (!cancellationToken.IsCancellationRequested))
        {
            

            actLine = await inStreamReader.ReadLineAsync();
        }
        
        throw new NotImplementedException();
    }

    internal static LogFileEntry? TryParseLogFileEntryFromLine(string line)
    {
        if (string.IsNullOrEmpty(line)) { return null; }
        
        var newLogEntry = new LogFileEntry();

        using var reader = new JsonTextReader(new StringReader(line));
        var currentProperty = (string?)null;
        while (reader.Read())
        {
            switch (reader.TokenType)
            {
                case JsonToken.PropertyName:
                    currentProperty = reader.Value as string;
                    break;
                    
                case JsonToken.String:
                    break;
                
                case JsonToken.Boolean:
                    break;
                    
            }
        }

        return newLogEntry;
    }

    /// <summary>
    /// Tries to parse a single line from compact json format.
    /// </summary>
    /// <param name="target">The target <see cref="LogFileEntry"/></param>
    /// <param name="propertyName">The name of the property from compact json line.</param>
    /// <param name="token">The current <see cref="JsonToken"/></param>
    /// <param name="value">The current value.</param>
    /// <exception cref="FormatException"></exception>
    internal static void TrySetJsonValue(LogFileEntry target, string propertyName, JsonToken token, object? value)
    {
        if (value == null)
        {
            throw new FormatException($"Null value for property {propertyName}");
        }
        
        switch (propertyName)
        {
            case "@t": // Timestamp
                if (token != JsonToken.String)
                {
                    throw new FormatException($"Expected string value on token {propertyName}, got {token}");
                }
                
                var valueString = (string)value;
                if (DateTimeOffset.TryParse(valueString, null, DateTimeStyles.RoundtripKind, out var parsedTimestamp))
                {
                    throw new FormatException("Unable to parse timestamp from @t field!");
                }

                target.Timestamp = parsedTimestamp;
                break;
            
            case "@mt": // Message
                if (token != JsonToken.String)
                {
                    throw new FormatException($"Expected string value on token {propertyName}, got {token}");
                }
                break;
            
            default:
                target.MetaData ??= new Dictionary<string, string>(6);
                target.MetaData[propertyName] = value.ToString() ?? string.Empty;
                break;
        }
    }
}