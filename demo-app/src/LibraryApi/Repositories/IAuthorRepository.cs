using LibraryApi.Models;

namespace LibraryApi.Repositories;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync();
    Task<Author?> GetByIdAsync(int id);
    Task<Author> AddAsync(Author author);
    Task UpdateAsync(Author author);
    Task DeleteAsync(Author author);
    Task<bool> ExistsAsync(int id);
}
