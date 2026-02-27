---
description: Review uncommitted changes against project quality standards
mode: agent
tools:
  - terminal
  - workspace
---

# Review Changes

## Step 1: Get Changes

Use @terminal to see what is being reviewed:
- `git status` — modified, staged, and untracked files
- `git diff --cached` — staged changes
- `git diff` — unstaged changes

## Step 2: Analyse Against Project Standards

Use @workspace context to review changes against the standards in
`.github/copilot-instructions.md` and `.github/instructions/dotnet.instructions.md`.

**Architecture**
- Do controllers delegate to services (not repositories or DbContext directly)?
- Is business logic in the service layer?
- Are new dependencies injected via constructor (not `new`)?

**Async / Await**
- Are all I/O operations async? No `.Result` or `.Wait()` blocking calls?
- Are new async methods named with the `Async` suffix?
- Is `DateTime.UtcNow` used instead of `DateTime.Now` for timestamps?

**Validation and Error Handling**
- Are inputs validated with Data Annotations or explicit checks?
- Are domain errors thrown as specific exceptions (`KeyNotFoundException`, etc.)?
- Are HTTP status codes correct? (201 for POST, 204 for DELETE, 409 for conflicts)

**Testing**
- Are there corresponding test stubs or tests for new methods?
- Are new services/repositories mockable via interfaces?

**Code Quality**
- Are there unused imports?
- Are there deeply nested blocks that could use early returns?
- Are there hard-coded magic strings that should be constants?

## Step 3: Provide Feedback

### 🔴 Critical Issues
Must be addressed before committing:
- Security vulnerabilities
- Bugs or logic errors
- Breaking changes

### 🟡 Important Improvements
Should be addressed:
- Missing validation
- Wrong HTTP status codes
- Pattern violations (controller accessing DB directly)

### 🟢 Nice-to-Haves
Consider but not blocking:
- Missing XML doc comments
- Missing `[ProducesResponseType]` attributes
- Additional test coverage

For each issue provide:
- **File and line** where the issue occurs
- **What** is wrong
- **Why** it matters
- **How to fix** it (with a corrected code snippet where helpful)

## Step 4: Summary

Provide:
- Overall quality assessment
- Issue count by priority
- Ready to commit or needs work
- Positive aspects worth calling out
