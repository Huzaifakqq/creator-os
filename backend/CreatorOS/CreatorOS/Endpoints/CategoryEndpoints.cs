using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(this WebApplication app)
    {
        app.MapGet("/api/marketplace/categories", async (CreatorOsContext db) =>
        {
            var cats = await db.MarketplaceCategories.Where(c => c.IsActive)
                .OrderBy(c => c.SortOrder)
                .Select(c => new { c.Id, c.Name, c.Slug, c.Description, c.Icon, c.ParentCategoryId, c.ListingCount })
                .ToListAsync();
            return Results.Ok(cats);
        }).WithName("GetCategories").WithTags("Categories");

        app.MapPost("/api/marketplace/categories", [Authorize] async (CreateCategoryRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var cat = new MarketplaceCategory
            {
                Id = Guid.NewGuid(), Name = req.Name, Slug = req.Name.ToLower().Replace(" ", "-"),
                Description = req.Description, Icon = req.Icon,
                ParentCategoryId = req.ParentCategoryId, IsActive = true,
                SortOrder = 0, ListingCount = 0, CreatedAt = DateTime.UtcNow
            };
            db.MarketplaceCategories.Add(cat);
            await db.SaveChangesAsync();
            return Results.Created($"/api/marketplace/categories/{cat.Id}", new { cat.Id, cat.Name });
        }).WithName("CreateCategory").WithTags("Categories");
    }
}

public record CreateCategoryRequest(string Name, string? Description, string? Icon, Guid? ParentCategoryId);
