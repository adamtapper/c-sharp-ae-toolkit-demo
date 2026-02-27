using LibraryApi.Data;
using LibraryApi.DTOs;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers;

// -----------------------------------------------------------------------
// DEMO NOTE: This controller is intentionally incomplete and contains
// several issues. It is the primary target for the Code Review chat mode
// and Copilot-assisted refactoring demos.
//
// Known issues to discover with Code Review chat mode:
//   1. Directly uses DbContext instead of a service/repository layer
//   2. AddReview missing validation that Rating is between 1 and 5
//   3. AddReview does not verify the bookId exists before inserting
//   4. DateTime.Now used instead of DateTime.UtcNow (timezone bug)
//   5. DeleteReview uses synchronous FirstOrDefault instead of async
//   6. DeleteReview does not verify the review belongs to bookId
//   7. AddReview returns 200 OK instead of 201 Created with Location header
//   8. DeleteReview returns 200 OK instead of 204 NoContent
//   9. No logging anywhere in this controller
//  10. No [ProducesResponseType] attributes on any action
// -----------------------------------------------------------------------

[ApiController]
[Route("api/books/{bookId:int}/reviews")]
public class ReviewsController(LibraryDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetReviews(int bookId)
    {
        var reviews = await context.Reviews
            .Where(r => r.BookId == bookId)
            .Select(r => new ReviewResponse(
                r.Id,
                r.BookId,
                r.Book.Title,
                r.ReviewerName,
                r.Rating,
                r.Comment,
                r.CreatedAt))
            .ToListAsync();

        return Ok(reviews);
    }

    [HttpPost]
    public async Task<IActionResult> AddReview(int bookId, [FromBody] CreateReviewRequest request)
    {
        // Issue 1: No check that the book exists
        // Issue 2: No validation that Rating is 1–5
        // Issue 3: Should go through a service, not directly to the DB

        var review = new Review
        {
            BookId = bookId,
            ReviewerName = request.ReviewerName,
            Rating = request.Rating,
            Comment = request.Comment,
            CreatedAt = DateTime.Now  // Bug: should be DateTime.UtcNow
        };

        context.Reviews.Add(review);
        await context.SaveChangesAsync();

        // Issue 4: Should return 201 Created with a Location header
        return Ok(review);
    }

    [HttpDelete("{reviewId:int}")]
    public async Task<IActionResult> DeleteReview(int bookId, int reviewId)
    {
        // Issue 5: Synchronous call in an async method
        var review = context.Reviews.FirstOrDefault(r => r.Id == reviewId);

        if (review == null)
        {
            return NotFound();
        }

        // Issue 6: Never verifies review.BookId == bookId
        // A caller could delete any review by guessing an ID, regardless of bookId in the URL

        context.Reviews.Remove(review);
        await context.SaveChangesAsync();

        // Issue 7: Should return 204 NoContent, not 200 OK
        return Ok();
    }
}
