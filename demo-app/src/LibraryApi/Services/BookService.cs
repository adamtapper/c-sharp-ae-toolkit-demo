using LibraryApi.DTOs;
using LibraryApi.Models;
using LibraryApi.Repositories;

namespace LibraryApi.Services;

public class BookService(IBookRepository bookRepository, IAuthorRepository authorRepository) : IBookService
{
    public async Task<IEnumerable<BookSummary>> GetAllBooksAsync()
    {
        var books = await bookRepository.GetAllAsync();
        return books.Select(MapToSummary);
    }

    public async Task<BookResponse?> GetBookByIdAsync(int id)
    {
        var book = await bookRepository.GetByIdAsync(id);
        return book is null ? null : MapToResponse(book);
    }

    public async Task<IEnumerable<BookSummary>> GetBooksByAuthorAsync(int authorId)
    {
        var books = await bookRepository.GetByAuthorIdAsync(authorId);
        return books.Select(MapToSummary);
    }

    public async Task<BookResponse> CreateBookAsync(CreateBookRequest request)
    {
        var author = await authorRepository.GetByIdAsync(request.AuthorId)
            ?? throw new KeyNotFoundException($"Author with ID {request.AuthorId} not found.");

        var existingBook = await bookRepository.GetByIsbnAsync(request.Isbn);
        if (existingBook is not null)
            throw new InvalidOperationException($"A book with ISBN '{request.Isbn}' already exists.");

        var book = new Book
        {
            Title = request.Title,
            Isbn = request.Isbn,
            PublishedYear = request.PublishedYear,
            Description = request.Description,
            Price = request.Price,
            AuthorId = request.AuthorId
        };

        var created = await bookRepository.AddAsync(book);
        created.Author = author;
        return MapToResponse(created);
    }

    public async Task<BookResponse?> UpdateBookAsync(int id, UpdateBookRequest request)
    {
        var book = await bookRepository.GetByIdAsync(id);
        if (book is null)
            return null;

        book.Title = request.Title;
        book.Isbn = request.Isbn;
        book.PublishedYear = request.PublishedYear;
        book.Description = request.Description;
        book.Price = request.Price;

        await bookRepository.UpdateAsync(book);
        return MapToResponse(book);
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await bookRepository.GetByIdAsync(id);
        if (book is null)
            return false;

        await bookRepository.DeleteAsync(book);
        return true;
    }

    private static BookResponse MapToResponse(Book book)
    {
        var avgRating = book.Reviews.Count > 0
            ? book.Reviews.Average(r => r.Rating)
            : 0.0;

        return new BookResponse(
            book.Id,
            book.Title,
            book.Isbn,
            book.PublishedYear,
            book.Description,
            book.Price,
            book.AuthorId,
            book.Author.FullName,
            Math.Round(avgRating, 1),
            book.Reviews.Count);
    }

    private static BookSummary MapToSummary(Book book)
    {
        var avgRating = book.Reviews.Count > 0
            ? book.Reviews.Average(r => r.Rating)
            : 0.0;

        return new BookSummary(
            book.Id,
            book.Title,
            book.Author.FullName,
            book.PublishedYear,
            book.Price,
            Math.Round(avgRating, 1));
    }
}
