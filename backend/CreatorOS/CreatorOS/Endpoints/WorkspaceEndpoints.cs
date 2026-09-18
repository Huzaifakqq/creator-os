using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Endpoints;

public static class WorkspaceEndpoints
{
    public static void MapWorkspaceEndpoints(this WebApplication app)
    {
        app.MapGet("/api/workspaces", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var workspaces = await db.Workspaces.Where(w => w.TenantId == tenantId)
                .Select(w => new { w.Id, w.Name, w.Slug, w.CustomDomain, w.CreatedAt })
                .ToListAsync();
            return Results.Ok(workspaces);
        }).WithName("GetWorkspaces").WithTags("Workspaces");

        app.MapPost("/api/workspaces", [Authorize] async (CreateWorkspaceRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var workspace = new Workspace
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, Name = req.Name,
                Slug = req.Name.ToLower().Replace(" ", "-"), CreatedAt = DateTime.UtcNow
            };
            db.Workspaces.Add(workspace);
            await db.SaveChangesAsync();
            return Results.Created($"/api/workspaces/{workspace.Id}", new { workspace.Id, workspace.Name, workspace.Slug });
        }).WithName("CreateWorkspace").WithTags("Workspaces");

        app.MapPut("/api/workspaces/{id}", [Authorize] async (Guid id, UpdateWorkspaceRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var workspace = await db.Workspaces.FirstOrDefaultAsync(w => w.Id == id && w.TenantId == tenantId);
            if (workspace == null) return Results.NotFound();
            if (req.Name != null) workspace.Name = req.Name;
            if (req.CustomDomain != null) workspace.CustomDomain = req.CustomDomain;
            if (req.Settings != null) workspace.Settings = req.Settings;
            workspace.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { workspace.Id, workspace.Name });
        }).WithName("UpdateWorkspace").WithTags("Workspaces");

        app.MapGet("/api/tenants/memberships", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var userId = Guid.Parse(System.Security.Claims.ClaimTypes.NameIdentifier);
            userId = Guid.Parse(http.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var memberships = await db.UserTenantMemberships.Where(m => m.UserId == userId)
                .Select(m => new { m.Id, m.TenantId, m.Role, m.Status, m.JoinedAt })
                .ToListAsync();
            return Results.Ok(memberships);
        }).WithName("GetMyMemberships").WithTags("Workspaces");

        app.MapGet("/api/marketplace/workspaces", async (CreatorOsContext db) =>
        {
            var tenants = await db.Tenants
                .Where(t => t.Status == "active")
                .Select(t => new
                {
                    t.Id,
                    t.Name,
                    t.Slug,
                    t.LogoUrl,
                    t.Plan,
                    MemberCount = db.UserTenantMemberships.Count(m => m.TenantId == t.Id && m.Status == "active")
                })
                .ToListAsync();
            return Results.Ok(tenants);
        }).WithName("GetPublicWorkspaces").WithTags("Marketplace");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CreateWorkspaceRequest(string Name);
public record UpdateWorkspaceRequest(string? Name, string? CustomDomain, string? Settings);
