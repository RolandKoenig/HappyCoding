namespace HappyCoding.AspNetWithAurelia.Domain.Model;

public class Genre
{
    public string Name { get; private set; } = string.Empty;

    private Genre()
    {
        
    }

    public Genre Create(string name)
    {
        var result = new Genre();
        result.Name = name;
        return result;
    }
}