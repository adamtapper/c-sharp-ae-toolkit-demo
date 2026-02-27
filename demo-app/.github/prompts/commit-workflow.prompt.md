---
description: Create a well-formatted Conventional Commit for staged changes
mode: agent
tools:
  - terminal
  - workspace
---

# Commit Workflow

## Step 1: Review What Will Be Committed

Use @terminal to run:
- `git status` — see staged and unstaged files
- `git diff --cached` — see the full staged diff

Analyse the output to understand the nature and scope of the changes.

## Step 2: Determine Commit Type and Scope

Based on the changes, identify:

| Type | When to use |
|------|-------------|
| `feat` | New feature or capability |
| `fix` | Bug fix |
| `refactor` | Code change that is neither a fix nor a feature |
| `test` | Adding or updating tests |
| `docs` | Documentation only changes |
| `chore` | Tooling, configuration, dependencies |

**Scope** (optional, but recommended): the layer or component affected.
Examples: `books`, `reviews`, `auth-service`, `db-context`

Note any **breaking changes** that affect the public API contract.

## Step 3: Draft the Commit Message

Use Conventional Commits format:

```
<type>(<scope>): <short description under 72 characters>

<optional body: explain WHAT changed and WHY, not HOW>

<optional footer: Closes #123, BREAKING CHANGE: ...>
```

**Rules:**
- Short description: lowercase, no trailing period, imperative mood ("add" not "added")
- Body: explain motivation, not mechanics
- Reference GitHub issues when applicable

**Examples:**
```
feat(reviews): add review ownership check to delete endpoint

fix(reviews): use DateTime.UtcNow instead of DateTime.Now for timestamps

refactor(reviews): extract ReviewService following Repository + Service pattern

test(book-service): add unit tests for CreateBookAsync edge cases
```

## Step 4: Create the Commit

Once the message is agreed, use @terminal:

```bash
git commit -m "type(scope): description" -m "Optional body text"
```

## Step 5: Confirm

Use @terminal to verify:
```bash
git log -1 --oneline
```

Show the commit SHA and message. Remind the user they can push with `git push`.
