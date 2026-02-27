using LibraryApi.Models;

namespace LibraryApi.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(int id);
    Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId);
    Task<Book?> GetByIsbnAsync(string isbn);
    Task<Book> AddAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(Book book);
    Task<bool> ExistsAsync(int id);
}
