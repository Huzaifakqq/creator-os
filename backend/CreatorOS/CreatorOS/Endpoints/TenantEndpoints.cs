using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CreatorOS.Endpoints;

public static class TenantEndpoints
{
    public static void MapTenantEndpoints(this WebApplication app)
    {
        app.MapPost("/api/tenants", [Authorize] async (CreateTenantRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var userId = http.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Results.Unauthorized();

            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = req.Name,
                Slug = req.Slug.ToLower().Replace(" ", "-"),
                Plan = "free",
                Status = "active",
                CreatedAt = DateTime.UtcNow
            };

            db.Tenants.Add(tenant);
            await db.SaveChangesAsync();

            var membership = new UserTenantMembership
            {
                Id = Guid.NewGuid(),
                TenantId = tenant.Id,
                UserId = Guid.Parse(userId),
                Role = "owner",
                Status = "active",
                CreatedAt = DateTime.UtcNow
            };

            db.UserTenantMemberships.Add(membership);
            await db.SaveChangesAsync();

            return Results.Ok(new { tenant.Id, tenant.Name, tenant.Slug });
        })
        .WithName("CreateTenant")
        .WithTags("Tenants");

        app.MapGet("/api/tenants", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var userId = http.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Results.Unauthorized();

            var tenants = await db.UserTenantMemberships
                .Where(m => m.UserId == Guid.Parse(userId) && m.Status == "active")
                .Join(db.Tenants, m => m.TenantId, t => t.Id, (m, t) => new { t.Id, t.Name, t.Slug, t.Plan, Role = m.Role })
                .ToListAsync();

            return Results.Ok(tenants);
        })
        .WithName("GetTenants")
        .WithTags("Tenants");
    }
}

public record CreateTenantRequest(string Name, string Slug);
