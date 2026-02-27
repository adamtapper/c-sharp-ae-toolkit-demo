using LibraryApi.DTOs;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers;

/// <summary>
/// Manages author records in the library system.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthorsController(IAuthorService authorService, ILogger<AuthorsController> logger) : ControllerBase
{
    /// <summary>Gets all authors.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AuthorResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var authors = await authorService.GetAllAuthorsAsync();
        return Ok(authors);
    }

    /// <summary>Gets a specific author by ID.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AuthorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var author = await authorService.GetAuthorByIdAsync(id);
        if (author is null)
        {
            logger.LogWarning("Author with ID {AuthorId} not found.", id);
            return NotFound(new { message = $"Author with ID {id} not found." });
        }
        return Ok(author);
    }

    /// <summary>Creates a new author record.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(AuthorResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAuthorRequest request)
    {
        var author = await authorService.CreateAuthorAsync(request);
        logger.LogInformation("Created author {AuthorId}: {FullName}", author.Id, author.FullName);
        return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
    }

    /// <summary>Updates an existing author record.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(AuthorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAuthorRequest request)
    {
        var author = await authorService.UpdateAuthorAsync(id, request);
        if (author is null)
            return NotFound(new { message = $"Author with ID {id} not found." });
        return Ok(author);
    }

    /// <summary>Deletes an author record.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await authorService.DeleteAuthorAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Author with ID {id} not found." });
        return NoContent();
    }
}
