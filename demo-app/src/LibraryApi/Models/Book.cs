namespace LibraryApi.Models;

public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Isbn { get; set; }
    public int PublishedYear { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
    public ICollection<Review> Reviews { get; set; } = [];
}
