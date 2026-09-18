using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CreatorOS.Endpoints;

public static class AppEndpoints
{
    public static void MapAppEndpoints(this WebApplication app)
    {
        app.MapGet("/api/apps", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });

            var apps = await db.Apps
                .Where(a => a.TenantId == tenantId)
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => new { a.Id, a.Name, a.Slug, a.Type, a.Status, a.PublishedVersion })
                .ToListAsync();
            return Results.Ok(apps);
        }).WithName("GetApps").WithTags("Apps");

        app.MapGet("/api/apps/{id}", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var app_ = await db.Apps.FirstOrDefaultAsync(a => a.Id == id && a.TenantId == tenantId);
            return app_ != null ? Results.Ok(app_) : Results.NotFound();
        }).WithName("GetApp").WithTags("Apps");

        app.MapPost("/api/apps", [Authorize] async (CreateAppRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });

            var workspace = await db.Workspaces.FirstOrDefaultAsync(w => w.TenantId == tenantId);
            if (workspace == null) return Results.BadRequest(new { error = "No workspace found. Create one first." });

            var app_ = new App
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, WorkspaceId = workspace.Id,
                Name = req.Name, Slug = req.Name.ToLower().Replace(" ", "-"),
                Type = req.Type, Status = "draft", CreatedAt = DateTime.UtcNow
            };
            db.Apps.Add(app_);
            await db.SaveChangesAsync();
            return Results.Created($"/api/apps/{app_.Id}", new { app_.Id, app_.Name });
        }).WithName("CreateApp").WithTags("Apps");

        app.MapPut("/api/apps/{id}", [Authorize] async (Guid id, UpdateAppRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var app_ = await db.Apps.FirstOrDefaultAsync(a => a.Id == id && a.TenantId == tenantId);
            if (app_ == null) return Results.NotFound();
            if (req.Name != null) app_.Name = req.Name;
            if (req.Description != null) app_.Description = req.Description;
            if (req.Config != null) app_.Config = req.Config;
            app_.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { app_.Id, app_.Name });
        }).WithName("UpdateApp").WithTags("Apps");

        app.MapDelete("/api/apps/{id}", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var app_ = await db.Apps.FirstOrDefaultAsync(a => a.Id == id && a.TenantId == tenantId);
            if (app_ == null) return Results.NotFound();
            db.Apps.Remove(app_);
            await db.SaveChangesAsync();
            return Results.Ok(new { message = "App deleted" });
        }).WithName("DeleteApp").WithTags("Apps");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CreateAppRequest(string Name, string Type);
public record UpdateAppRequest(string? Name, string? Description, string? Config);
