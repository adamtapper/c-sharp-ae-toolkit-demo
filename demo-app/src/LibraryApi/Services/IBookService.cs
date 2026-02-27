using LibraryApi.DTOs;

namespace LibraryApi.Services;

public interface IBookService
{
    Task<IEnumerable<BookSummary>> GetAllBooksAsync();
    Task<BookResponse?> GetBookByIdAsync(int id);
    Task<IEnumerable<BookSummary>> GetBooksByAuthorAsync(int authorId);
    Task<BookResponse> CreateBookAsync(CreateBookRequest request);
    Task<BookResponse?> UpdateBookAsync(int id, UpdateBookRequest request);
    Task<bool> DeleteBookAsync(int id);
}
