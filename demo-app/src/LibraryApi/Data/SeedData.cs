using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Data;

public static class SeedData
{
    public static async Task InitializeAsync(LibraryDbContext context)
    {
        if (await context.Authors.AnyAsync())
            return;

        var authors = new[]
        {
            new Author
            {
                FirstName = "Martin",
                LastName = "Fowler",
                Biography = "British software developer, author, and international public speaker specializing in object-oriented analysis and design, UML, patterns, and agile software development.",
                BirthDate = new DateOnly(1963, 12, 18)
            },
            new Author
            {
                FirstName = "Robert",
                LastName = "Martin",
                Biography = "American software engineer and instructor, best known for promoting many software design principles including SOLID, and for being a co-author of the Agile Manifesto.",
                BirthDate = new DateOnly(1952, 12, 5)
            },
            new Author
            {
                FirstName = "Eric",
                LastName = "Evans",
                Biography = "Author of the influential book 'Domain-Driven Design: Tackling Complexity in the Heart of Software', which established the DDD methodology.",
                BirthDate = null
            }
        };

        await context.Authors.AddRangeAsync(authors);
        await context.SaveChangesAsync();

        var books = new[]
        {
            new Book
            {
                Title = "Refactoring: Improving the Design of Existing Code",
                Isbn = "9780201485677",
                PublishedYear = 2018,
                Description = "A practical guide to refactoring code safely and incrementally, with a comprehensive catalog of refactoring patterns.",
                Price = 49.99m,
                AuthorId = authors[0].Id
            },
            new Book
            {
                Title = "Patterns of Enterprise Application Architecture",
                Isbn = "9780321127426",
                PublishedYear = 2002,
                Description = "A catalog of patterns for enterprise application architecture, covering layers, domain logic, data source, and more.",
                Price = 59.99m,
                AuthorId = authors[0].Id
            },
            new Book
            {
                Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
                Isbn = "9780132350884",
                PublishedYear = 2008,
                Description = "Filled with case studies, the book teaches how to write code that is readable, understandable, and maintainable.",
                Price = 44.99m,
                AuthorId = authors[1].Id
            },
            new Book
            {
                Title = "The Clean Coder: A Code of Conduct for Professional Programmers",
                Isbn = "9780137081073",
                PublishedYear = 2011,
                Description = "A guide to professional conduct and practices for software developers.",
                Price = 39.99m,
                AuthorId = authors[1].Id
            },
            new Book
            {
                Title = "Domain-Driven Design: Tackling Complexity in the Heart of Software",
                Isbn = "9780321125217",
                PublishedYear = 2003,
                Description = "Presents a systematic approach to domain-driven design, centering the development on an evolving model of the core domain.",
                Price = 54.99m,
                AuthorId = authors[2].Id
            }
        };

        await context.Books.AddRangeAsync(books);
        await context.SaveChangesAsync();

        var reviews = new[]
        {
            new Review
            {
                BookId = books[0].Id,
                ReviewerName = "Alice Chen",
                Rating = 5,
                Comment = "Essential reading for any developer. Changed how I think about code.",
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new Review
            {
                BookId = books[0].Id,
                ReviewerName = "Bob Singh",
                Rating = 4,
                Comment = "Very practical. The catalog of refactoring patterns is invaluable.",
                CreatedAt = DateTime.UtcNow.AddDays(-15)
            },
            new Review
            {
                BookId = books[2].Id,
                ReviewerName = "Carol Williams",
                Rating = 5,
                Comment = "A must-read for every programmer. Timeless advice on clean coding.",
                CreatedAt = DateTime.UtcNow.AddDays(-45)
            },
            new Review
            {
                BookId = books[4].Id,
                ReviewerName = "Dave Johnson",
                Rating = 4,
                Comment = "Dense but rewarding. Takes multiple reads to fully appreciate the depth.",
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            }
        };

        await context.Reviews.AddRangeAsync(reviews);
        await context.SaveChangesAsync();
    }
}
