using System.ComponentModel.DataAnnotations;

namespace LibraryApi.DTOs;

public record AuthorResponse(
    int Id,
    string FirstName,
    string LastName,
    string FullName,
    string? Biography,
    DateOnly? BirthDate,
    int BookCount);

public record CreateAuthorRequest(
    [Required, StringLength(100, MinimumLength = 1)] string FirstName,
    [Required, StringLength(100, MinimumLength = 1)] string LastName,
    string? Biography,
    DateOnly? BirthDate);

public record UpdateAuthorRequest(
    [Required, StringLength(100, MinimumLength = 1)] string FirstName,
    [Required, StringLength(100, MinimumLength = 1)] string LastName,
    string? Biography,
    DateOnly? BirthDate);
