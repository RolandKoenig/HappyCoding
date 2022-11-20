using System.Text.Json;

namespace HappyCoding.YamlParsing.Cases;

public abstract class CaseBase
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions =
        new JsonSerializerOptions(JsonSerializerDefaults.General)
        {
            WriteIndented = true
        };
    
    public abstract Task ParseAsync();
    
    /// <summary>
    /// Gets a reading stream to the given 
    /// </summary>
    protected async Task<string> GetEmbeddedResourceStringAsync(string embeddedResourceName)
    {
        var type = this.GetType();
        var resName = $"{type.Namespace}.{embeddedResourceName}";
        
        await using var resStream = type.Assembly.GetManifestResourceStream(resName);
        if (resStream == null)
        {
            throw new FileNotFoundException(resName);
        }

        using var resStreamReader = new StreamReader(resStream);
        return await resStreamReader.ReadToEndAsync();
    }

    protected void WriteCaseStart(string caseName)
    {
        Console.WriteLine();
        Console.WriteLine("#####################");
        Console.WriteLine($"Case: {caseName}");
    }

    protected void WriteYaml(string fullYamlString)
    {
        Console.WriteLine();
        Console.WriteLine("# Original Yaml:");
        Console.WriteLine(fullYamlString);
    }

    protected void WriteCaseEnd<TModel>(TModel model)
    {
        Console.WriteLine();
        Console.WriteLine("# Deserialized Yaml as Json:");
        Console.WriteLine(JsonSerializer.Serialize(model, _jsonSerializerOptions));
    }
}