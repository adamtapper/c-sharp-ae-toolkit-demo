using LibraryApi.Data;
using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repositories;

public class BookRepository(LibraryDbContext context) : IBookRepository
{
    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await context.Books
            .Include(b => b.Author)
            .Include(b => b.Reviews)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await context.Books
            .Include(b => b.Author)
            .Include(b => b.Reviews)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Book>> GetByAuthorIdAsync(int authorId)
    {
        return await context.Books
            .Include(b => b.Author)
            .Include(b => b.Reviews)
            .Where(b => b.AuthorId == authorId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Book?> GetByIsbnAsync(string isbn)
    {
        return await context.Books
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Isbn == isbn);
    }

    public async Task<Book> AddAsync(Book book)
    {
        context.Books.Add(book);
        await context.SaveChangesAsync();
        return book;
    }

    public async Task UpdateAsync(Book book)
    {
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Book book)
    {
        context.Books.Remove(book);
        await context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Books.AnyAsync(b => b.Id == id);
    }
}
