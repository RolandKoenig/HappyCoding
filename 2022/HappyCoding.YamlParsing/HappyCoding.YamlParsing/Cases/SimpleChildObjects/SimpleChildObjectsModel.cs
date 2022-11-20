namespace HappyCoding.YamlParsing.Cases.SimpleChildObjects;

public class SimpleChildObjectsModel
{
    public string Name { get; set; } = string.Empty;

    public int Age { get; set; }
    
    public DateTime BirthDate { get; set; }
    
    public WorkModel? Work { get; set; }
    
    public WorkModel? PrevWork { get; set; }
}

public class WorkModel
{
    public string Title { get; set; } = string.Empty;

    public string Company { get; set; } = string.Empty;
    
    public int DurationYears { get; set; }
}