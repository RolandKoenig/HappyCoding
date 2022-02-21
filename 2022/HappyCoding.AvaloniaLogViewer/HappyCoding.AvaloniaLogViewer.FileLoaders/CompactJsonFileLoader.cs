using System.Globalization;
using HappyCoding.AvaloniaLogViewer.Domain;
using HappyCoding.AvaloniaLogViewer.Domain.Exceptions;
using HappyCoding.AvaloniaLogViewer.Domain.Model;
using Newtonsoft.Json;

namespace HappyCoding.AvaloniaLogViewer.FileLoaders;

public class CompactJsonFileLoader : ILogFileLoader
{
    private static readonly string[] FILE_EXTENSIONS = { "clef" };
    
    public string LoaderName => "Compact Json";

    public IEnumerable<string> FileExtensions => FILE_EXTENSIONS;
    
    public async IAsyncEnumerable<LogFileEntry> LoadFileAsync(string inputFile, CancellationToken cancellationToken)
    {
        await using var inStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var inStreamReader = new StreamReader(inStream);

        var actLine = await inStreamReader.ReadLineAsync();
        var lineNumber = 0;
        while ((actLine != null) &&
               (!cancellationToken.IsCancellationRequested))
        {
            lineNumber++;

            var newLogFileEntry = TryParseLogFileEntryFromLine(actLine, lineNumber);
            if (newLogFileEntry != null)
            {
                yield return newLogFileEntry;
            }
            
            actLine = await inStreamReader.ReadLineAsync();
        }
    }

    internal static LogFileEntry? TryParseLogFileEntryFromLine(string line, int lineNumber)
    {
        if (string.IsNullOrEmpty(line)) { return null; }

        try
        {
            var newLogEntry = new LogFileEntry();

            using var reader = new JsonTextReader(new StringReader(line));
            var currentProperty = (string?) null;
            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.PropertyName:
                        currentProperty = reader.Value as string;
                        break;

                    case JsonToken.String:
                    case JsonToken.Boolean:
                    case JsonToken.Date:
                    case JsonToken.Float:
                    case JsonToken.Integer:
                        if (currentProperty == null)
                        {
                            throw new LogFileParseException(
                                $"Unexpected json token {reader.TokenType} on position {reader.LinePosition} in compact json, line: {line}");
                        }
                        TrySetJsonValue(newLogEntry, currentProperty, reader.TokenType, reader.Value, lineNumber);
                        break;
                }
            }

            return newLogEntry;
        }
        catch (LogFileParseException)
        {
            throw;
        }
        catch (Exception)
        {
            throw new LogFileParseException($"Unexpected error while parsing line number {lineNumber}. Line={line}");
        }
    }

    /// <summary>
    /// Tries to parse a single line from compact json format.
    /// </summary>
    /// <param name="target">The target <see cref="LogFileEntry"/></param>
    /// <param name="propertyName">The name of the property from compact json line.</param>
    /// <param name="token">The current <see cref="JsonToken"/></param>
    /// <param name="value">The current value.</param>
    /// <param name="lineNumber">Current line number</param>
    /// <exception cref="FormatException"></exception>
    internal static void TrySetJsonValue(LogFileEntry target, string propertyName, JsonToken token, object? value, int lineNumber)
    {
        if (value == null)
        {
            throw new FormatException($"Null value for property {propertyName}, line number={lineNumber}");
        }
        
        switch (propertyName)
        {
            case "@t": // Timestamp
                switch (token)
                {
                    case JsonToken.Date:
                        var valueDateTime = (DateTime)value;
                        target.Timestamp = new DateTimeOffset(valueDateTime.ToUniversalTime(), TimeSpan.Zero);
                        break;
                    
                    case JsonToken.String:
                        var valueString = (string) value;
                        if (!DateTimeOffset.TryParse(valueString, null, DateTimeStyles.RoundtripKind, out var parsedTimestamp))
                        {
                            throw new LogFileParseException($"Unable to parse timestamp from @t field, line number={lineNumber}, value={valueString}");
                        }
                        target.Timestamp = parsedTimestamp;
                        break;
                    
                    default:
                        throw new LogFileParseException($"Expected string value on token {propertyName}, got {token}, expected {JsonToken.Date} or {JsonToken.String}, line number={lineNumber}");
                }
                break;
            
            case "@mt": // Message
                if (token != JsonToken.String)
                {
                    throw new FormatException($"Expected string value on token {propertyName}, got {token}, line number={lineNumber}");
                }
                break;
            
            default:
                target.MetaData ??= new Dictionary<string, string>(6);
                target.MetaData[propertyName] = value.ToString() ?? string.Empty;
                break;
        }
    }
}