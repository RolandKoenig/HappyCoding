namespace HappyCoding.YamlParsing.Cases.YamlWithReferences;

public class YamlWithReferencesModel
{
    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }
    
    public DateTime BirthDate { get; set; }

    public string City { get; set; } = string.Empty;

    public string City2 { get; set; } = string.Empty;
}