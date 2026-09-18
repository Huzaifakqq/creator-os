using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Endpoints;

public static class MarketplaceListEndpoints
{
    public static void MapMarketplaceListEndpoints(this WebApplication app)
    {
        app.MapGet("/api/marketplace/lists", async (HttpContext http, CreatorOsContext db) =>
        {
            var categories = await db.MarketplaceCategories.Where(c => c.IsActive)
                .OrderBy(c => c.SortOrder)
                .Select(c => new { c.Id, c.Name, c.Slug, c.Description, c.Icon, c.ListingCount })
                .ToListAsync();
            return Results.Ok(categories);
        }).WithName("GetMarketplaceLists").WithTags("Marketplace");

        app.MapGet("/api/marketplace/lists/{id}", async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var cat = await db.MarketplaceCategories.FirstOrDefaultAsync(c => c.Id == id && c.IsActive);
            return cat != null ? Results.Ok(cat) : Results.NotFound();
        }).WithName("GetMarketplaceList").WithTags("Marketplace");
    }
}
