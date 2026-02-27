using LibraryApi.DTOs;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

/// <summary>
/// Manages the library's book catalog.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BooksController(IBookService bookService, ILogger<BooksController> logger) : ControllerBase
{
    /// <summary>Gets all books in the catalog.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookSummary>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var books = await bookService.GetAllBooksAsync();
        return Ok(books);
    }

    /// <summary>Gets a specific book by ID.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await bookService.GetBookByIdAsync(id);
        if (book is null)
        {
            logger.LogWarning("Book with ID {BookId} not found.", id);
            return NotFound(new { message = $"Book with ID {id} not found." });
        }
        return Ok(book);
    }

    /// <summary>Gets all books by a specific author.</summary>
    [HttpGet("by-author/{authorId:int}")]
    [ProducesResponseType(typeof(IEnumerable<BookSummary>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByAuthor(int authorId)
    {
        var books = await bookService.GetBooksByAuthorAsync(authorId);
        return Ok(books);
    }

    /// <summary>Adds a new book to the catalog.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateBookRequest request)
    {
        try
        {
            var book = await bookService.CreateBookAsync(request);
            logger.LogInformation("Created book {BookId}: {Title}", book.Id, book.Title);
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    /// <summary>Updates an existing book.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBookRequest request)
    {
        var book = await bookService.UpdateBookAsync(id, request);
        if (book is null)
            return NotFound(new { message = $"Book with ID {id} not found." });
        return Ok(book);
    }

    /// <summary>Removes a book from the catalog.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await bookService.DeleteBookAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Book with ID {id} not found." });
        return NoContent();
    }
}
