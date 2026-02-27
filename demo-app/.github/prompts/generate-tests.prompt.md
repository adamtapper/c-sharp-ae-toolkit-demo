---
description: Generate comprehensive xUnit tests for a .NET service or controller
mode: ask
---

Generate complete xUnit unit tests for the selected or attached file.

## Instructions

1. Examine the class under test and identify all public methods.

2. For each method, generate tests covering:
   - **Happy path** — valid inputs, successful outcomes
   - **Not found / null cases** — when the repository or service returns `null`
   - **Error cases** — exceptions thrown due to invalid state (duplicate ISBN, author not found, etc.)
   - **Edge cases** — empty collections, boundary values

3. Use this exact structure for the test class:

```csharp
public class {ClassName}Tests
{
    // Mock dependencies
    private readonly Mock<IDependency> _dependencyMock = new();
    private readonly {ClassName} _sut;

    public {ClassName}Tests()
    {
        _sut = new {ClassName}(_dependencyMock.Object);
    }

    [Fact]
    public async Task {MethodName}_{Condition}_{ExpectedOutcome}()
    {
        // Arrange
        ...

        // Act
        var result = await _sut.{MethodName}(...);

        // Assert
        result.Should()...;
    }
}
```

4. Use **Moq** for all dependencies. Use **FluentAssertions** for all assertions.

5. Replace all `throw new NotImplementedException(...)` stubs with complete test implementations.

6. Add any additional tests for edge cases you identify that are not already stubbed.

7. Ensure tests are independent — no shared mutable state, no relying on execution order.
