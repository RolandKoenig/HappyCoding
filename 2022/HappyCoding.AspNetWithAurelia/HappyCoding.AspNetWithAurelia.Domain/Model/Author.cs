namespace HappyCoding.AspNetWithAurelia.Domain.Model;

public class Author
{
    public string Name { get; private set; } = string.Empty;

    private Author()
    {
        
    }

    public Author Create(string name)
    {
        var result = new Author();
        result.Name = name;
        return result;
    }
}