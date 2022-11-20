namespace HappyCoding.YamlParsing.Cases.SimpleChildCollections;

public class SimpleChildCollectionsModel
{
    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }
    
    public DateTime BirthDate { get; set; }

    public string[] ProgrammingLanguages { get; set; } = Array.Empty<string>();

    public AdditionalSkill[] AdditionalSkills { get; set; } = Array.Empty<AdditionalSkill>();
}

public class AdditionalSkill
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Level { get; set; } = string.Empty;
}