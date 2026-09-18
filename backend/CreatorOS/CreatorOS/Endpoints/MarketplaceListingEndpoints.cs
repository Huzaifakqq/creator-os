using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Endpoints;

public static class MarketplaceListingEndpoints
{
    public static void MapMarketplaceListingEndpoints(this WebApplication app)
    {
        app.MapGet("/api/marketplace/listings", async (HttpContext http, CreatorOsContext db, string? category, string? sort) =>
        {
            var query = db.MarketplaceListings.Where(l => l.Status == "approved").AsQueryable();
            if (!string.IsNullOrEmpty(category))
                query = query.Where(l => l.Category == category);
            query = sort switch
            {
                "price_asc" => query.OrderBy(l => l.PriceCents),
                "price_desc" => query.OrderByDescending(l => l.PriceCents),
                "rating" => query.OrderByDescending(l => l.Rating),
                _ => query.OrderByDescending(l => l.TotalSales)
            };
            var listings = await query.Take(50).Select(l => new
            {
                l.Id, l.Title, l.Description, l.ShortDescription, l.Category,
                l.PriceCents, l.LicenseType, l.Rating, l.RatingCount, l.TotalSales, l.ThumbnailUrl
            }).ToListAsync();
            return Results.Ok(listings);
        }).WithName("GetMarketplaceListings").WithTags("Marketplace");

        app.MapGet("/api/marketplace/listings/{id}", async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var listing = await db.MarketplaceListings.FirstOrDefaultAsync(l => l.Id == id && l.Status == "approved");
            return listing != null ? Results.Ok(listing) : Results.NotFound();
        }).WithName("GetMarketplaceListing").WithTags("Marketplace");

        app.MapPost("/api/marketplace/listings", [Authorize] async (CreateListingRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var listing = new MarketplaceListing
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, ProductId = req.ProductId,
                Title = req.Title, ShortDescription = req.ShortDescription,
                Description = req.Description ?? "", Category = req.Category ?? "other",
                PriceCents = req.PriceCents, LicenseType = req.LicenseType ?? "standard",
                Status = "pending_review", Rating = 0, RatingCount = 0,
                TotalSales = 0, TotalRevenueCents = 0, Version = "1.0.0",
                CreatedAt = DateTime.UtcNow
            };
            db.MarketplaceListings.Add(listing);
            await db.SaveChangesAsync();
            return Results.Created($"/api/marketplace/listings/{listing.Id}", new { listing.Id, listing.Title });
        }).WithName("CreateMarketplaceListing").WithTags("Marketplace");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CreateListingRequest(Guid ProductId, string Title, string? ShortDescription, string? Description, string? Category, long PriceCents, string? LicenseType);
