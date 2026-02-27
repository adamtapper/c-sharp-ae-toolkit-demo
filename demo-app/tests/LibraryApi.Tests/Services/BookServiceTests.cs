using FluentAssertions;
using LibraryApi.DTOs;
using LibraryApi.Models;
using LibraryApi.Repositories;
using LibraryApi.Services;
using Moq;

namespace LibraryApi.Tests.Services;

// -----------------------------------------------------------------------
// DEMO NOTE: These tests are intentionally incomplete stubs.
//
// DEMO STEP: Open this file in VS Code, then use the Test Generator
// chat mode (or generate-tests.prompt.md) to have GitHub Copilot
// complete each test with proper Arrange/Act/Assert structure.
//
// Expected: Copilot should generate xUnit tests using Moq and
// FluentAssertions following the patterns in .github/copilot-instructions.md
// -----------------------------------------------------------------------

public class BookServiceTests
{
    private readonly Mock<IBookRepository> _bookRepositoryMock = new();
    private readonly Mock<IAuthorRepository> _authorRepositoryMock = new();
    private readonly BookService _sut;

    public BookServiceTests()
    {
        _sut = new BookService(_bookRepositoryMock.Object, _authorRepositoryMock.Object);
    }

    // ---- Helper factories ------------------------------------------------

    private static Author MakeAuthor(int id = 1) => new()
    {
        Id = id,
        FirstName = "Martin",
        LastName = "Fowler",
        Books = []
    };

    private static Book MakeBook(int id = 1, int authorId = 1, Author? author = null) => new()
    {
        Id = id,
        Title = "Refactoring",
        Isbn = "9780201485677",
        PublishedYear = 2018,
        Price = 49.99m,
        AuthorId = authorId,
        Author = author ?? MakeAuthor(authorId),
        Reviews = []
    };

    // ---- GetAllBooksAsync ------------------------------------------------

    [Fact]
    public async Task GetAllBooksAsync_WhenBooksExist_ReturnsSummaryForEachBook()
    {
        // TODO: Complete this test using GitHub Copilot Test Generator chat mode
        // Arrange: mock _bookRepositoryMock.GetAllAsync() to return [MakeBook()]
        // Act:     var result = await _sut.GetAllBooksAsync()
        // Assert:  result should contain one BookSummary with the expected values
        throw new NotImplementedException("Complete with Copilot Test Generator");
    }

    [Fact]
    public async Task GetAllBooksAsync_WhenNoBooksExist_ReturnsEmptyCollection()
    {
        // TODO: Complete this test using GitHub Copilot Test Generator chat mode
        throw new NotImplementedException("Complete with Copilot Test Generator");
    }

    // ---- GetBookByIdAsync ------------------------------------------------

    [Fact]
    public async Task GetBookByIdAsync_WithValidId_ReturnsBookResponse()
    {
        // TODO: Complete this test using GitHub Copilot Test Generator chat mode
        throw new NotImplementedException("Complete with Copilot Test Generator");
    }

    [Fact]
    public async Task GetBookByIdAsync_WithUnknownId_ReturnsNull()
    {
        // TODO: Complete this test using GitHub Copilot Test Generator chat mode
        throw new NotImplementedException("Complete with Copilot Test Generator");
    }

    // ---- CreateBookAsync ------------------------------------------------

    [Fact]
    public async Task CreateBookAsync_WithValidRequest_CreatesAndReturnsBook()
    {
        // TODO: Complete this test using GitHub Copilot Test Generator chat mode
        // Hint: mock both _authorRepositoryMock.GetByIdAsync and
        //       _bookRepositoryMock.GetByIsbnAsync (returns null) and AddAsync
        throw new NotImplementedException("Complete with Copilot Test Generator");
    }

    [Fact]
    public async Task CreateBookAsync_WhenAuthorNotFound_ThrowsKeyNotFoundException()
    {
        // TODO: Complete this test using GitHub Copilot Test Generator chat mode
        throw new NotImplementedException("Complete with Copilot Test Generator");
    }

    [Fact]
    public async Task CreateBookAsync_WithDuplicateIsbn_ThrowsInvalidOperationException()
    {
        // TODO: Complete this test using GitHub Copilot Test Generator chat mode
        throw new NotImplementedException("Complete with Copilot Test Generator");
    }

    // ---- UpdateBookAsync ------------------------------------------------

    [Fact]
    public async Task UpdateBookAsync_WithValidIdAndRequest_UpdatesAndReturnsBook()
    {
        // TODO: Complete this test using GitHub Copilot Test Generator chat mode
        throw new NotImplementedException("Complete with Copilot Test Generator");
    }

    [Fact]
    public async Task UpdateBookAsync_WithUnknownId_ReturnsNull()
    {
        // TODO: Complete this test using GitHub Copilot Test Generator chat mode
        throw new NotImplementedException("Complete with Copilot Test Generator");
    }

    // ---- DeleteBookAsync ------------------------------------------------

    [Fact]
    public async Task DeleteBookAsync_WithValidId_DeletesBookAndReturnsTrue()
    {
        // TODO: Complete this test using GitHub Copilot Test Generator chat mode
        throw new NotImplementedException("Complete with Copilot Test Generator");
    }

    [Fact]
    public async Task DeleteBookAsync_WithUnknownId_ReturnsFalse()
    {
        // TODO: Complete this test using GitHub Copilot Test Generator chat mode
        throw new NotImplementedException("Complete with Copilot Test Generator");
    }
}
