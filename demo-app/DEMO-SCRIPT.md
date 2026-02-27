# Library API — C# AE Toolkit Demo Script

This script walks through a live demo of the **C# AE Toolkit** capabilities using
GitHub Copilot and the Library API application.

---

## Prerequisites

| Requirement | Version |
|---|---|
| .NET SDK | 9.0+ |
| VS Code | Latest |
| GitHub Copilot extension | Latest (Copilot + Copilot Chat) |
| Git | Any recent version |

**Important:** Open the `demo-app/` folder as your VS Code workspace root (not the
parent `c-sharp-ae-toolkit/` folder). Copilot reads `.github/copilot-instructions.md`
relative to the workspace root.

```
File → Open Folder → .../c-sharp-ae-toolkit/demo-app/
```

---

## Part 1 — Run the API (5 min)

### 1.1 Start the server

```bash
cd src/LibraryApi
dotnet run
```

The API starts on `http://localhost:5000`. The Swagger UI opens automatically at the
root URL.

### 1.2 Explore the seeded data via Swagger

The database is pre-seeded with:
- **3 authors** — Martin Fowler, Robert Martin, Eric Evans
- **5 books** — Refactoring, PEAA, Clean Code, The Clean Coder, DDD
- **4 reviews**

Try these in Swagger:
1. `GET /api/books` — see all books with average ratings
2. `GET /api/books/1` — get Refactoring with full detail
3. `GET /api/books/1` → note the `reviewCount` and `averageRating` fields
4. `GET /api/books/by-author/1` — Fowler's books only
5. `GET /api/authors` — all authors with book counts
6. `GET /api/books/1/reviews` — reviews for Refactoring (via ReviewsController)

### 1.3 Point out the architecture

Quick orientation before the demos:
- `BooksController` and `AuthorsController` are **well-implemented** — use these as the
  "before" reference
- `ReviewsController` is **intentionally flawed** — the demo target for code review

---

## Part 2 — Copilot Instructions in Action (10 min)

**Toolkit piece:** `.github/copilot-instructions.md` + `.github/instructions/dotnet.instructions.md`

**Goal:** Show how project-wide Copilot instructions shape code generation.

### 2.1 Add a new field with Copilot inline completion

Open `src/LibraryApi/DTOs/BookDtos.cs`. At the end of `CreateBookRequest`, start
typing a new field:

```csharp
// Start typing: "Genre" then tab
```

Notice Copilot suggests `[StringLength(...)]` annotations automatically — it learned
this pattern from the instructions and the existing fields.

### 2.2 Ask Copilot to add a new endpoint

Open Copilot Chat and ask:

```
Add a GET endpoint to BooksController that searches books by title
(case-insensitive partial match). Follow the same patterns as the existing endpoints.
```

Observe that Copilot:
- Adds a `[HttpGet("search")]` action
- Uses `IBookService` (not DbContext directly)
- Adds `[ProducesResponseType]` attributes
- Uses structured logging
- Uses `IEnumerable<BookSummary>` as the return type

This is the instructions working — without them, Copilot might go to DbContext directly.

### 2.3 Ask Copilot to add the corresponding service method

```
Add the SearchByTitleAsync(string query) method to IBookService and BookService
to support the new endpoint.
```

Notice it:
- Adds the `Async` suffix
- Uses LINQ `.Where()` with `Contains()` (case-insensitive)
- Returns `IEnumerable<BookSummary>` matching the interface

---

## Part 3 — Code Review Chat Mode (10 min)

**Toolkit piece:** `.github/chatmodes/code-reviewer.chatmode.md`

**Goal:** Use the Code Reviewer chat mode to audit `ReviewsController`.

### 3.1 Open the Code Review chat mode

In VS Code:
1. Open Copilot Chat panel
2. Click the mode selector (top of the chat panel)
3. Select **"Code Review Expert"** (the `code-reviewer.chatmode.md` mode)

### 3.2 Run a review on ReviewsController

With `ReviewsController.cs` open in the editor, type:

```
Review this controller file.
```

Copilot should produce a structured review identifying:

| Priority | Issue |
|---|---|
| 🔴 Critical | `DeleteReview` never checks `review.BookId == bookId` — any review can be deleted with a guessed ID |
| 🔴 Critical | `DateTime.Now` used instead of `DateTime.UtcNow` — timezone bug in stored data |
| 🔴 Critical | `FirstOrDefault` (synchronous) called inside an async method |
| 🟡 Important | No check that the book exists before inserting a review |
| 🟡 Important | `Rating` has no range validation — values outside 1–5 are silently stored |
| 🟡 Important | `AddReview` returns `200 OK` instead of `201 Created` with Location header |
| 🟡 Important | `DeleteReview` returns `200 OK` instead of `204 NoContent` |
| 🟡 Important | `DbContext` injected directly — should use a service layer |
| 🟢 Minor | No logging |
| 🟢 Minor | No `[ProducesResponseType]` attributes |

### 3.3 Ask Copilot to fix the critical issues

```
Fix the critical and important issues you identified, following the patterns
in BooksController.
```

Watch Copilot refactor the controller. Key things to point out to the audience:
- It creates a `IReviewService` / `ReviewService` layer
- It fixes the async bug
- It fixes the `DateTime.Now` bug
- It adds the ownership check to `DeleteReview`

---

## Part 4 — Test Generator Chat Mode (10 min)

**Toolkit piece:** `.github/chatmodes/test-generator.chatmode.md`

**Goal:** Complete the stub tests in `BookServiceTests.cs` using Copilot.

### 4.1 Open the Test Generator chat mode

1. Open Copilot Chat
2. Switch to **"Test Generation Expert"** mode

### 4.2 Generate tests for BookService

Open `tests/LibraryApi.Tests/Services/BookServiceTests.cs`. Ask:

```
Complete all the NotImplementedException stubs in this file with full
xUnit tests using Moq and FluentAssertions.
```

Copilot should generate tests that:
- Use the `MakeBook()` and `MakeAuthor()` helpers already in the file
- Mock `_bookRepositoryMock` and `_authorRepositoryMock`
- Verify exception messages with `.WithMessage("*...")`
- Use `Times.Once` and `Times.Never` to verify repository interactions

### 4.3 Run the generated tests

```bash
cd ../../   # back to demo-app root
dotnet test
```

Point out: the tests that Copilot generated follow the naming convention and
FluentAssertions patterns from the instructions — without the instructions Copilot
would likely use `Assert.Equal()` and less descriptive names.

### 4.4 Use the generate-tests prompt (alternative approach)

For a quick demo of prompt files, open `BookService.cs`, then in Copilot Chat:

1. Click the paperclip icon and attach `BookService.cs`
2. Type `/generate-tests` — this triggers `prompts/generate-tests.prompt.md`

Copilot reads the prompt file and generates a new complete test class from scratch.
This is useful when starting fresh rather than completing stubs.

---

## Part 5 — API Documenter Chat Mode (8 min)

**Toolkit piece:** `.github/chatmodes/api-documenter.chatmode.md`

**Goal:** Generate XML doc comments and an endpoint summary table.

### 5.1 Switch to API Documenter mode

Select **"API Documentation Expert"** from the chat mode selector.

### 5.2 Generate a summary table for the whole API

```
Generate a markdown endpoint summary table for all three controllers in this project.
```

Copilot should produce a table with method, route, description, success codes, and
error codes for all 13 endpoints.

### 5.3 Add XML doc comments to ReviewDtos

Open `src/LibraryApi/DTOs/ReviewDtos.cs`. Ask:

```
Add XML documentation comments to CreateReviewRequest explaining each parameter,
its constraints, and what validation is missing (note the Rating field has no
Range attribute).
```

The documenter mode will add `/// <param>` comments and note the missing validation
as a documentation concern.

---

## Part 6 — Architecture Reviewer Chat Mode (8 min)

**Toolkit piece:** `.github/chatmodes/architecture-reviewer.chatmode.md`

**Goal:** Get an architectural assessment of the Repository + Service pattern.

### 6.1 Switch to Architecture Reviewer mode

Select **"Architecture Review Expert"** from the chat mode selector.

### 6.2 Request an architecture review

```
Review the overall architecture of this .NET API. Evaluate the choice to use
Repository + Service layers over accessing DbContext directly in controllers.
What are the trade-offs for a demo project vs. a production API?
```

Expect Copilot to:
- Acknowledge testability benefits of the repository abstraction
- Point out that EF Core `DbContext` is already a unit-of-work + repository
- Suggest what production additions would be needed (versioning, health checks, problem details)
- Recommend an ADR document for the architectural decision

### 6.3 Ask about the missing ReviewService

```
Compare ReviewsController to BooksController from an architectural perspective.
What risks does the current ReviewsController design introduce at scale?
```

---

## Part 7 — Prompt Commands (5 min)

**Toolkit pieces:** `.github/prompts/review-changes.prompt.md`, `commit-workflow.prompt.md`

### 7.1 Stage a change

Make a small edit — for example, add a `// TODO: Add rate limiting` comment to
`ReviewsController.cs`. Then stage it:

```bash
git add src/LibraryApi/Controllers/ReviewsController.cs
```

### 7.2 Run the review-changes prompt

In Copilot Chat (switch back to default mode), use:

```
/review-changes
```

Copilot reads the prompt file, calls `git diff --cached` via the terminal tool, and
provides a structured review of your staged changes.

### 7.3 Run the commit-workflow prompt

```
/commit-workflow
```

Copilot:
1. Inspects `git status` and `git diff --cached`
2. Suggests a Conventional Commit message (e.g., `chore(reviews): add rate limiting todo comment`)
3. Optionally executes the commit if you approve

---

## Part 8 — Rules Manager Context (optional, 3 min)

**Toolkit piece:** `../../rules-manager/` in the parent toolkit

This is a discussion-only slide, no live demo needed.

Explain to the audience:
- For a single developer or small team, the instructions in `.github/copilot-instructions.md`
  are sufficient — the Library API demonstrates this well
- For a team of 8+ developers with multiple tech stacks, the **Rules Manager** module
  provides a router-based approach to apply different rule sets per file type
- Demonstrate the `examples/rules/copilot/` directory structure as the source of the
  instructions we used in this demo

---

## Demo Summary and Toolkit Map

| Demo Step | Toolkit Component Used |
|---|---|
| 2 — Instructions shape Copilot | `examples/rules/copilot/` (base + tech rules) |
| 3 — Code Review | `examples/chat-modes/copilot/code-quality/code-reviewer.chatmode.md` |
| 4 — Test Generation | `examples/chat-modes/copilot/testing/test-generator.chatmode.md` |
| 5 — API Documentation | `examples/chat-modes/copilot/documentation/api-documenter.chatmode.md` |
| 6 — Architecture Review | `examples/chat-modes/copilot/architecture/architecture-reviewer.chatmode.md` |
| 7 — Prompt Commands | `examples/commands/copilot/code-quality/review-changes.prompt.md` |
| 7 — Commit Workflow | `examples/commands/copilot/git-workflows/commit-workflow.prompt.md` |

**Not demonstrated (but available in the toolkit):**
- `ai-initializer/` — use this first on a greenfield project to generate the instructions
  that were pre-built for this demo
- `context-refresher/` — run this when the codebase drifts from its documentation
- `interaction-analyzer/` — run this if Copilot starts giving unhelpful suggestions
- `scratch-management-utilities/` — for maintaining context across long Copilot sessions

---

## Frequently Asked Questions

**Q: Where do the chat modes appear in VS Code?**
A: In GitHub Copilot Chat, click the mode selector dropdown at the top of the chat
panel. Chat modes defined in `.github/chatmodes/*.chatmode.md` appear there automatically
(requires a recent version of the Copilot extension).

**Q: Do prompt files need a specific name?**
A: Files in `.github/prompts/` ending in `.prompt.md` are recognised by Copilot as
reusable prompt commands and are accessible via `/filename` in chat.

**Q: The in-memory database resets on restart — is that intentional?**
A: Yes, for demo simplicity. The seed data in `SeedData.cs` re-populates on every
start. To persist data, swap `UseInMemoryDatabase` in `Program.cs` for
`UseSqlite("Data Source=library.db")` and add a migration.

**Q: Can I run this against a real SQL Server?**
A: Yes. Add `Microsoft.EntityFrameworkCore.SqlServer`, update `Program.cs` to use
`UseSqlServer(connectionString)`, run `dotnet ef migrations add InitialCreate`,
then `dotnet ef database update`.
