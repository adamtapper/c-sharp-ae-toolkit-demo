# Library API — C# AE Toolkit Demo Script

A focused 8-minute demo of the **C# AE Toolkit** using GitHub Copilot and the Library API.

The narrative arc: *explore the API → catch a design violation → catch the bugs inside it.*

---

## Prerequisites

| Requirement | Version |
|---|---|
| .NET SDK | 10.0+ |
| VS Code | Latest |
| GitHub Copilot extension | Latest (Copilot + Copilot Chat) |

**Important:** Open the `demo-app/` folder as your VS Code workspace root (not the
parent repo root). Copilot reads `.github/copilot-instructions.md` relative to the
workspace root.

```
File → Open Folder → .../c-sharp-ae-toolkit-demo/demo-app/
```

---

## Part 1 — Run the API and tour Swagger (~2 min)

### 1.1 Start the server

```bash
cd src/LibraryApi
dotnet run
```

The API starts on `http://localhost:5000`. Swagger opens at the root URL.

### 1.2 Show 3 endpoints

The database is pre-seeded with 3 authors, 5 books, and 4 reviews.

| Endpoint | What to point out |
|---|---|
| `GET /api/books` | Returns `averageRating` and `reviewCount` — computed fields |
| `GET /api/books/by-author/1` | Fowler's books; Copilot instructions shape how this was built |
| `GET /api/books/1/reviews` | Routed through `ReviewsController` — the intentionally flawed file |

### 1.3 Set up the story

> "The `BooksController` and `AuthorsController` are well-implemented.
> `ReviewsController` has real problems — it was written without following
> the project's architecture rules. Let's use the AI toolkit to find out exactly what's wrong."

---

## Part 2 — Architecture Reviewer (~3 min)

**Toolkit piece:** `.github/chatmodes/architecture-reviewer.chatmode.md`

**What it shows:** The AI identifies a *design-level* violation before we look at a single line of business logic.

### 2.1 Switch to Architecture Review Expert mode

In VS Code:
1. Open Copilot Chat panel
2. Click the mode selector at the top of the panel
3. Select **"Architecture Review Expert"**

### 2.2 Ask for an architecture review focused on the controllers

```
Review the overall architecture of this API. I'm particularly interested in
how the three controllers compare — do they all follow the same layering rules?
```

**What to expect:**
- Identifies that `BooksController` and `AuthorsController` use `IBookService` / `IAuthorService` correctly
- Flags that `ReviewsController` injects `LibraryDbContext` directly — a violation of the Controller → Service → Repository layering rule documented in `copilot-instructions.md`
- Notes the missing service abstraction makes `ReviewsController` untestable in isolation
- May recommend an ADR to document the layering decision

**Point out to the audience:** The agent read the project's own `.github/copilot-instructions.md` to know what the rules are — it's evaluating the code against the team's stated standards, not generic advice.

### 2.3 Ask about production readiness

```
What would need to change before this API was production-ready?
```

Expect a concise list: global exception handling, API versioning, health checks, authentication, distributed caching. This shows the agent distinguishing demo-appropriate simplicity from production requirements.

---

## Part 3 — Code Reviewer on ReviewsController (~4 min)

**Toolkit piece:** `.github/chatmodes/code-reviewer.chatmode.md`

**What it shows:** The AI catches concrete, consequential bugs — not style nits.

### 3.1 Switch to Code Review Expert mode

1. Open `src/LibraryApi/Controllers/ReviewsController.cs` in the editor
2. In Copilot Chat, switch to **"Code Review Expert"** mode

### 3.2 Run the review

```
Review this controller file.
```

The `.NET-Specific Review Checklist` in the chat mode targets exactly the bugs planted here. Copilot should produce a prioritised report:

| Priority | Issue |
|---|---|
| 🔴 Critical | `DeleteReview` never verifies `review.BookId == bookId` — any review can be deleted with a guessed ID |
| 🔴 Critical | `DateTime.Now` used instead of `DateTime.UtcNow` — timezone corruption in stored data |
| 🔴 Critical | `FirstOrDefault` (synchronous EF Core) called inside an `async` method — blocks a thread pool thread |
| 🟡 Important | No check that the referenced book exists before inserting a review |
| 🟡 Important | `Rating` has no `[Range(1, 5)]` attribute — out-of-bounds values are silently stored |
| 🟡 Important | `AddReview` returns `200 OK` instead of `201 Created` with a `Location` header |
| 🟡 Important | `DeleteReview` returns `200 OK` instead of `204 NoContent` |
| 🟡 Important | `DbContext` injected directly — architecture violation caught again, now at the code level |
| 🟢 Minor | No structured logging |
| 🟢 Minor | No `[ProducesResponseType]` attributes |

**Point out to the audience:** The three critical issues are real bugs with real consequences — a security flaw, a data integrity flaw, and a concurrency flaw. The AI caught all three without being told where to look.

### 3.3 Ask Copilot to fix the critical issues

```
Fix the critical and important issues you identified, following the patterns
used in BooksController and the architecture rules in copilot-instructions.md.
```

Watch Copilot:
- Create `IReviewService` and `ReviewService` to move business logic out of the controller
- Replace `DateTime.Now` with `DateTime.UtcNow`
- Replace `FirstOrDefault` with `await ... FirstOrDefaultAsync`
- Add the ownership check to `DeleteReview`
- Return `CreatedAtAction` from `AddReview` and `NoContent` from `DeleteReview`

This demonstrates the *full loop*: instructions shape what the AI knows, agents surface violations against those instructions, and the AI uses the good code as the reference when fixing the bad code.

---

## Demo Summary

| Part | Time | Toolkit Component |
|---|---|---|
| 1 — Swagger tour | ~2 min | Live app |
| 2 — Architecture Reviewer | ~3 min | `.github/chatmodes/architecture-reviewer.chatmode.md` |
| 3 — Code Reviewer | ~4 min | `.github/chatmodes/code-reviewer.chatmode.md` |

**Why this combination:**
- Architecture review → code review is a natural macro-to-micro drill-down
- The architecture review primes the audience ("ReviewsController breaks the layering rules")
- The code review delivers the punchline (three critical bugs inside that same file)
- Together they show two different lenses the toolkit provides: design correctness and implementation correctness

---

## What else is in the toolkit

The demo uses two of the chat modes. The full toolkit also includes:

| Component | What it does |
|---|---|
| `chatmodes/test-generator.chatmode.md` | Completes stub tests using xUnit + Moq + FluentAssertions |
| `chatmodes/api-documenter.chatmode.md` | Generates XML doc comments and endpoint summary tables |
| `prompts/review-changes.prompt.md` | Reviews staged git changes (`/review-changes`) |
| `prompts/commit-workflow.prompt.md` | Inspects diff and suggests a Conventional Commit message |
| `ai-initializer/` | Generates the `copilot-instructions.md` for a greenfield project |
| `context-refresher/` | Updates instructions when the codebase drifts from its documentation |

---

## FAQ

**Q: Where do chat modes appear in VS Code?**
In GitHub Copilot Chat, click the mode selector at the top of the panel. Files in
`.github/chatmodes/*.chatmode.md` appear there automatically (requires a recent version
of the Copilot extension).

**Q: The in-memory database resets on restart — is that intentional?**
Yes. Seed data in `SeedData.cs` re-populates on every start. To persist data, swap
`UseInMemoryDatabase` in `Program.cs` for `UseSqlite("Data Source=library.db")`.

**Q: Can I run this against SQL Server?**
Yes. Add `Microsoft.EntityFrameworkCore.SqlServer`, update `Program.cs` to use
`UseSqlServer(connectionString)`, run `dotnet ef migrations add InitialCreate`,
then `dotnet ef database update`.
