# GitHub Copilot Instructions — Library API Demo

This file provides project-wide context for GitHub Copilot. It is derived from the
**C# AE Toolkit** (see `../../examples/` in this repository) and customised for the
Library API demo application.

---

## Project Overview

**Library API** is an ASP.NET Core 9 Web API that manages a library catalog of books,
authors, and reviews. It uses:

- **ASP.NET Core 9** with minimal hosting model
- **Entity Framework Core** with an in-memory database (for demo purposes)
- **Repository + Service pattern** for separation of concerns
- **xUnit + Moq + FluentAssertions** for testing

**Structure:**
```
src/LibraryApi/
  Controllers/    — HTTP layer (thin; delegates to services)
  Services/       — Business logic (IBookService, IAuthorService)
  Repositories/   — Data access (IBookRepository, IAuthorRepository)
  Models/         — EF Core entities (Book, Author, Review)
  DTOs/           — Request/response records (BookDtos, AuthorDtos, ReviewDtos)
  Data/           — DbContext and seed data
tests/LibraryApi.Tests/
  Services/       — Unit tests for service layer using Moq
```

---

## Code Style and Conventions

Follow .NET naming conventions strictly:
- **Classes, Methods, Properties:** `PascalCase`
- **Interfaces:** `IPascalCase` (with `I` prefix)
- **Local variables, parameters:** `camelCase`
- **Private fields:** `_camelCase` (underscore prefix)
- **Async methods:** Always add `Async` suffix

Enable and respect nullable reference types (`<Nullable>enable</Nullable>`).

Use `ArgumentNullException.ThrowIfNull()` for null guards (.NET 6+).

Use modern C# features: primary constructors, collection expressions (`[]`),
pattern matching, switch expressions, `is not null`, and target-typed `new`.

Prefer `record` types for immutable DTOs.

---

## Architecture Rules

Always use the service layer from controllers — never inject `DbContext` or
repositories directly into controllers (see `ReviewsController` for a counter-example).

Controllers are thin HTTP adapters:
- Validate input (model binding handles this via `[ApiController]`)
- Call a service method
- Map the service result to an HTTP response
- Log warnings/errors with structured logging (`_logger.LogWarning(...)`)
- Return appropriate status codes with `ProducesResponseType` attributes

Services contain all business logic:
- Validate business rules (duplicate ISBN, author exists, etc.)
- Throw domain exceptions (`KeyNotFoundException`, `InvalidOperationException`)
- Map between domain models and DTOs

Repositories are pure data access:
- No business logic
- Return domain models, not DTOs
- Use `AsNoTracking()` on read-only queries

---

## Code Quality Standards

Always handle edge cases: null, empty collections, boundary values.

Avoid deep nesting — use early returns and guard clauses.

Use LINQ methods (`Where`, `Select`, `FirstOrDefault`) over manual loops.

Use `IEnumerable<T>` for parameters, `List<T>` for mutable returns.

Never include hard-coded secrets or credentials.

Use parameterised queries (EF Core handles this automatically).

Remove unused imports.

Do not add comments that restate what the code obviously does.

---

## Testing Standards

Use **xUnit** with `[Fact]` for single-scenario tests and `[Theory]` for
parameterised tests.

Use **Moq** for mocking dependencies (`Mock<IBookRepository>`).

Use **FluentAssertions** for readable assertions (`result.Should().Be(...)`).

Test method naming: `MethodName_Condition_ExpectedOutcome`
Example: `CreateBookAsync_WhenAuthorNotFound_ThrowsKeyNotFoundException`

Structure every test as **Arrange / Act / Assert** with blank lines between sections.

Mock at service boundaries (repositories), not internal implementation details.

---

## Git Commit Format

Use Conventional Commits:
```
<type>(<scope>): <short description>

<optional body>
```

Types: `feat`, `fix`, `refactor`, `test`, `docs`, `chore`

Example: `feat(books): add filtering by published year`
