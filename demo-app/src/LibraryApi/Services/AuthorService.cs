using LibraryApi.DTOs;
using LibraryApi.Models;
using LibraryApi.Repositories;

namespace LibraryApi.Services;

public class AuthorService(IAuthorRepository authorRepository) : IAuthorService
{
    public async Task<IEnumerable<AuthorResponse>> GetAllAuthorsAsync()
    {
        var authors = await authorRepository.GetAllAsync();
        return authors.Select(MapToResponse);
    }

    public async Task<AuthorResponse?> GetAuthorByIdAsync(int id)
    {
        var author = await authorRepository.GetByIdAsync(id);
        return author is null ? null : MapToResponse(author);
    }

    public async Task<AuthorResponse> CreateAuthorAsync(CreateAuthorRequest request)
    {
        var author = new Author
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Biography = request.Biography,
            BirthDate = request.BirthDate
        };

        await authorRepository.AddAsync(author);
        return MapToResponse(author);
    }

    public async Task<AuthorResponse?> UpdateAuthorAsync(int id, UpdateAuthorRequest request)
    {
        var author = await authorRepository.GetByIdAsync(id);
        if (author is null)
            return null;

        author.FirstName = request.FirstName;
        author.LastName = request.LastName;
        author.Biography = request.Biography;
        author.BirthDate = request.BirthDate;

        await authorRepository.UpdateAsync(author);
        return MapToResponse(author);
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var author = await authorRepository.GetByIdAsync(id);
        if (author is null)
            return false;

        await authorRepository.DeleteAsync(author);
        return true;
    }

    private static AuthorResponse MapToResponse(Author author) =>
        new(
            author.Id,
            author.FirstName,
            author.LastName,
            author.FullName,
            author.Biography,
            author.BirthDate,
            author.Books.Count);
}
