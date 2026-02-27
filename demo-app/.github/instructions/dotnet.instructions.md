---
applyTo: "**/*.cs"
---

# .NET / C# Instructions

**Source:** Adapted from `examples/rules/copilot/` in the C# AE Toolkit.
**Applies to:** All `.cs` files in this project.

---

Use primary constructors for classes with only dependency-injected fields.

Always use `async`/`await` for I/O operations. Name async methods with the `Async` suffix.

Return `Task` directly (without `async`/`await`) when no intermediate processing is needed — avoids an unnecessary state machine allocation.

Enable and honour nullable reference types. Use `?` for nullable parameters and return types. Use `??` and `?.` operators instead of explicit null checks where cleaner.

Use `ArgumentNullException.ThrowIfNull()` as the standard null guard pattern (.NET 6+).

Prefer `switch` expressions over `switch` statements for value selection.

Use collection expressions (`[]`) instead of `new List<T>()` or `Array.Empty<T>()` where the target type is inferred.

Use LINQ (`Where`, `Select`, `FirstOrDefault`, `Any`) over manual `foreach` loops when transforming or querying collections.

Always call `.ToListAsync()` or `.ToArrayAsync()` to materialise EF Core queries — never leave queries as `IQueryable` past a repository boundary.

Use `AsNoTracking()` on read-only EF Core queries for better performance.

Follow C# XML doc comment format (`/// <summary>`) on all public methods, constructors, and non-obvious properties.

Use structured logging: `_logger.LogInformation("Created {EntityType} {Id}", "Book", id)`.

Throw specific exceptions (`KeyNotFoundException`, `InvalidOperationException`, custom domain exceptions) rather than generic `Exception`.

Return `IEnumerable<T>` or `IReadOnlyCollection<T>` from service methods; use `List<T>` internally.

Use `record` types for DTOs and value objects that should be immutable.
