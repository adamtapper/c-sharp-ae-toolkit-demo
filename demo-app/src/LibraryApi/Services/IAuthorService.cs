using LibraryApi.DTOs;

namespace LibraryApi.Services;

public interface IAuthorService
{
    Task<IEnumerable<AuthorResponse>> GetAllAuthorsAsync();
    Task<AuthorResponse?> GetAuthorByIdAsync(int id);
    Task<AuthorResponse> CreateAuthorAsync(CreateAuthorRequest request);
    Task<AuthorResponse?> UpdateAuthorAsync(int id, UpdateAuthorRequest request);
    Task<bool> DeleteAuthorAsync(int id);
}
