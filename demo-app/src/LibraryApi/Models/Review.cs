namespace LibraryApi.Models;

public class Review
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
    public required string ReviewerName { get; set; }
    public int Rating { get; set; } // Expected range: 1–5
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
