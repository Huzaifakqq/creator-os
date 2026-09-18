using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Endpoints;

public static class ReviewEndpoints
{
    public static void MapReviewEndpoints(this WebApplication app)
    {
        app.MapGet("/api/reviews", async (Guid? listingId, HttpContext http, CreatorOsContext db) =>
        {
            var query = db.MarketplaceReviews.Where(r => r.IsVisible).AsQueryable();
            if (listingId.HasValue) query = query.Where(r => r.ListingId == listingId);
            var reviews = await query.OrderByDescending(r => r.CreatedAt).Take(50)
                .Select(r => new { r.Id, r.Rating, r.Title, r.Comment, r.ListingId, r.IsVerifiedPurchase, r.HelpfulCount, r.CreatedAt })
                .ToListAsync();
            return Results.Ok(reviews);
        }).WithName("GetReviews").WithTags("Reviews");

        app.MapPost("/api/reviews", [Authorize] async (CreateReviewRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var review = new MarketplaceReview
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, ListingId = req.ListingId,
                CustomerId = Guid.Empty, Rating = req.Rating, Title = req.Title,
                Comment = req.Body, IsVerifiedPurchase = false, IsVisible = true,
                HelpfulCount = 0, CreatedAt = DateTime.UtcNow
            };
            db.MarketplaceReviews.Add(review);
            await db.SaveChangesAsync();
            return Results.Created($"/api/reviews/{review.Id}", new { review.Id, review.Rating });
        }).WithName("CreateReview").WithTags("Reviews");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CreateReviewRequest(Guid ListingId, int Rating, string? Title, string? Body);
