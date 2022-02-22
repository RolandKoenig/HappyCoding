using Light.GuardClauses;

namespace HappyCoding.AspNetWithAurelia.Domain.Model;

public class Book
{
    public string Title { get; private set; } = null!;

    public Genre Genre { get; private set; } = null!;

    public Author Author { get; private set; } = null!;
    
    public string? Description { get; set; }
    
    private Book()
    {
        
    }

    public Book Create(string title, Genre genre, Author author)
    {
        title.MustNotBeNullOrEmpty();
        
        var result = new Book();
        result.Title = title;
        result.Genre = genre;
        result.Author = author;
        return result;
    }
}