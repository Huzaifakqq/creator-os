using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CreatorOS.Endpoints;

public static class PageEndpoints
{
    public static void MapPageEndpoints(this WebApplication app)
    {
        app.MapGet("/api/apps/{appId}/pages", [Authorize] async (Guid appId, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var pages = await db.Pages.Where(p => p.AppId == appId && p.TenantId == tenantId)
                .OrderBy(p => p.SortOrder)
                .Select(p => new { p.Id, p.Title, p.Slug, p.PageType, p.IsPublished })
                .ToListAsync();
            return Results.Ok(pages);
        }).WithName("GetPages").WithTags("Pages");

        app.MapPost("/api/apps/{appId}/pages", [Authorize] async (Guid appId, CreatePageRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var page = new Page
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, AppId = appId,
                Title = req.Title, Slug = req.Title.ToLower().Replace(" ", "-"),
                PageType = req.PageType ?? "custom", IsPublished = false,
                SortOrder = 0, CreatedAt = DateTime.UtcNow
            };
            db.Pages.Add(page);
            await db.SaveChangesAsync();
            return Results.Created($"/api/pages/{page.Id}", new { page.Id, page.Title });
        }).WithName("CreatePage").WithTags("Pages");

        app.MapPut("/api/pages/{id}", [Authorize] async (Guid id, UpdatePageRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var page = await db.Pages.FirstOrDefaultAsync(p => p.Id == id && p.TenantId == tenantId);
            if (page == null) return Results.NotFound();
            if (req.Title != null) page.Title = req.Title;
            if (req.SeoTitle != null) page.SeoTitle = req.SeoTitle;
            if (req.SeoDescription != null) page.SeoDescription = req.SeoDescription;
            if (req.CustomCss != null) page.CustomCss = req.CustomCss;
            page.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { page.Id, page.Title });
        }).WithName("UpdatePage").WithTags("Pages");

        app.MapDelete("/api/pages/{id}", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var page = await db.Pages.FirstOrDefaultAsync(p => p.Id == id && p.TenantId == tenantId);
            if (page == null) return Results.NotFound();
            db.Pages.Remove(page);
            await db.SaveChangesAsync();
            return Results.Ok(new { message = "Page deleted" });
        }).WithName("DeletePage").WithTags("Pages");

        app.MapGet("/api/pages/{pageId}/blocks", [Authorize] async (Guid pageId, HttpContext http, CreatorOsContext db) =>
        {
            var blocks = await db.PageBlocks.Where(b => b.PageId == pageId)
                .OrderBy(b => b.SortOrder)
                .Select(b => new { b.Id, b.BlockType, b.Config, b.SortOrder, b.IsVisible })
                .ToListAsync();
            return Results.Ok(blocks);
        }).WithName("GetBlocks").WithTags("Pages");

        app.MapPost("/api/pages/{pageId}/blocks", [Authorize] async (Guid pageId, CreateBlockRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var block = new PageBlock
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, PageId = pageId,
                BlockType = req.BlockType, Config = req.Config, SortOrder = req.SortOrder,
                IsVisible = true, CreatedAt = DateTime.UtcNow
            };
            db.PageBlocks.Add(block);
            await db.SaveChangesAsync();
            return Results.Created($"/api/blocks/{block.Id}", new { block.Id, block.BlockType });
        }).WithName("CreateBlock").WithTags("Pages");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CreatePageRequest(string Title, string? PageType);
public record UpdatePageRequest(string? Title, string? SeoTitle, string? SeoDescription, string? CustomCss);
public record CreateBlockRequest(string BlockType, string Config, int SortOrder);
