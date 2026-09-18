using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Endpoints;

public static class MarketplaceEnhancedEndpoints
{
    public static void MapMarketplaceEnhancedEndpoints(this WebApplication app)
    {
        app.MapPut("/api/marketplace/listings/{id}/status", [Authorize] async (Guid id, ListingStatusRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var listing = await db.MarketplaceListings.FirstOrDefaultAsync(l => l.Id == id);
            if (listing == null) return Results.NotFound();
            listing.Status = req.Status;
            if (req.Status == "approved")
            {
                listing.ReviewedAt = DateTime.UtcNow;
                listing.ReviewedBy = Guid.Parse(http.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            }
            else if (req.Status == "rejected") { listing.RejectionReason = req.Reason; listing.ReviewedAt = DateTime.UtcNow; }
            listing.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { listing.Id, listing.Status });
        }).WithName("UpdateListingStatus").WithTags("Marketplace");

        app.MapPost("/api/marketplace/listings/{id}/remix", [Authorize] async (Guid id, RemixRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var listing = await db.MarketplaceListings.FirstOrDefaultAsync(l => l.Id == id);
            if (listing == null) return Results.NotFound();
            var remix = new TemplateRemix
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value,
                OriginalTemplateId = id, RemixAppId = Guid.Empty,
                LicenseType = "standard", RoyaltyPercent = 0, CreatedAt = DateTime.UtcNow
            };
            db.TemplateRemixes.Add(remix);
            listing.TotalSales += 1;
            listing.TotalRevenueCents += listing.PriceCents;
            await db.SaveChangesAsync();
            return Results.Created($"/api/marketplace/remixes/{remix.Id}", new { remix.Id, message = "Template remixed" });
        }).WithName("RemixTemplate").WithTags("Marketplace");

        app.MapGet("/api/marketplace/listings/{id}/metrics", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var metrics = await db.MarketplaceListingMetrics.Where(m => m.ListingId == id)
                .OrderByDescending(m => m.CreatedAt).Take(30)
                .Select(m => new { m.Id, m.ViewCount, m.ClickCount, m.PurchaseCount, m.RevenueCents, m.Date }).ToListAsync();
            return Results.Ok(metrics);
        }).WithName("GetListingMetrics").WithTags("Marketplace");

        app.MapGet("/api/marketplace/remixes", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var remixes = await db.TemplateRemixes.Where(r => r.TenantId == tenantId)
                .OrderByDescending(r => r.CreatedAt).Take(20)
                .Select(r => new { r.Id, r.OriginalTemplateId, r.LicenseType, r.RoyaltyPercent, r.CreatedAt }).ToListAsync();
            return Results.Ok(remixes);
        }).WithName("GetRemixes").WithTags("Marketplace");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record ListingStatusRequest(string Status, string? Reason);
public record RemixRequest(string? CreatorName);
