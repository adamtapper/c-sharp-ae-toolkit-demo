using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs;

public record ReviewResponse(
    int Id,
    int BookId,
    string BookTitle,
    string ReviewerName,
    int Rating,
    string? Comment,
    DateTime CreatedAt);

public record CreateReviewRequest(
    [Required, StringLength(200, MinimumLength = 1)] string ReviewerName,
    int Rating,  // Intentionally missing [Range(1,5)] — demo target for code review
    string? Comment);
