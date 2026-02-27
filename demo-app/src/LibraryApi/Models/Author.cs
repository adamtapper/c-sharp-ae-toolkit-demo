namespace LibraryApi.Models;

public class Author
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Biography { get; set; }
    public DateOnly? BirthDate { get; set; }
    public ICollection<Book> Books { get; set; } = [];

    public string FullName => $"{FirstName} {LastName}";
}
