using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Endpoints;

public static class SubscriptionEndpoints
{
    public static void MapSubscriptionEndpoints(this WebApplication app)
    {
        app.MapGet("/api/subscriptions", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var subs = await db.Subscriptions.Where(s => s.TenantId == tenantId)
                .OrderByDescending(s => s.CreatedAt)
                .Select(s => new { s.Id, s.Status, s.CurrentPeriodStart, s.CurrentPeriodEnd, s.CreatedAt })
                .ToListAsync();
            return Results.Ok(subs);
        }).WithName("GetSubscriptions").WithTags("Subscriptions");

        app.MapPost("/api/subscriptions", [Authorize] async (CreateSubscriptionRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var sub = new Subscription
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, CustomerId = req.CustomerId,
                ProductId = req.ProductId, Status = "active",
                CurrentPeriodStart = DateTime.UtcNow,
                CurrentPeriodEnd = DateTime.UtcNow.AddMonths(1),
                CreatedAt = DateTime.UtcNow
            };
            db.Subscriptions.Add(sub);
            await db.SaveChangesAsync();
            return Results.Created($"/api/subscriptions/{sub.Id}", new { sub.Id, sub.Status });
        }).WithName("CreateSubscription").WithTags("Subscriptions");

        app.MapPut("/api/subscriptions/{id}/cancel", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var sub = await db.Subscriptions.FirstOrDefaultAsync(s => s.Id == id && s.TenantId == tenantId);
            if (sub == null) return Results.NotFound();
            sub.Status = "canceled";
            sub.CanceledAt = DateTime.UtcNow;
            sub.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { sub.Id, sub.Status });
        }).WithName("CancelSubscription").WithTags("Subscriptions");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CreateSubscriptionRequest(Guid CustomerId, Guid ProductId);
