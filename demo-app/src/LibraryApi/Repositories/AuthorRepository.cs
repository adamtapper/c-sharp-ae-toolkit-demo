using LibraryApi.Data;
using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Repositories;

public class AuthorRepository(LibraryDbContext context) : IAuthorRepository
{
    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await context.Authors
            .Include(a => a.Books)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Author?> GetByIdAsync(int id)
    {
        return await context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Author> AddAsync(Author author)
    {
        context.Authors.Add(author);
        await context.SaveChangesAsync();
        return author;
    }

    public async Task UpdateAsync(Author author)
    {
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Author author)
    {
        context.Authors.Remove(author);
        await context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.Authors.AnyAsync(a => a.Id == id);
    }
}
