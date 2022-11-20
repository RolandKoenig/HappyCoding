namespace HappyCoding.YamlParsing.Cases.ObjectWithDictionary;

public class ObjectWithDictionaryModel
{
    public string ContainerName { get; set; } = string.Empty;

    public string ImageName { get; set; } = string.Empty;
    
    public Dictionary<string, string>? Metadata { get; set; }
}