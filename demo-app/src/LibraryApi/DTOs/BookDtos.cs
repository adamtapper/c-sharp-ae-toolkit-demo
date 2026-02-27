using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs;

public record BookResponse(
    int Id,
    string Title,
    string Isbn,
    int PublishedYear,
    string? Description,
    decimal Price,
    int AuthorId,
    string AuthorName,
    double AverageRating,
    int ReviewCount);

public record CreateBookRequest(
    [Required, StringLength(300, MinimumLength = 1)] string Title,
    [Required, StringLength(13, MinimumLength = 10)] string Isbn,
    [Range(1450, 2100)] int PublishedYear,
    string? Description,
    [Range(0.01, 9999.99)] decimal Price,
    [Range(1, int.MaxValue)] int AuthorId);

public record UpdateBookRequest(
    [Required, StringLength(300, MinimumLength = 1)] string Title,
    [Required, StringLength(13, MinimumLength = 10)] string Isbn,
    [Range(1450, 2100)] int PublishedYear,
    string? Description,
    [Range(0.01, 9999.99)] decimal Price);

public record BookSummary(
    int Id,
    string Title,
    string AuthorName,
    int PublishedYear,
    decimal Price,
    double AverageRating);
