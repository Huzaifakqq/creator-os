using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CreatorOS.Endpoints;

public static class AdminEndpoints
{
    public static void MapAdminEndpoints(this WebApplication app)
    {
        app.MapGet("/api/admin/dashboard", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var totalTenants = await db.Tenants.CountAsync();
            var totalUsers = await db.Users.CountAsync();
            var totalOrders = await db.Orders.CountAsync();
            var totalRevenue = await db.Orders.Where(o => o.Status == "completed").SumAsync(o => o.TotalCents);
            var activeSubscriptions = await db.Subscriptions.CountAsync(s => s.Status == "active");
            var pendingListings = await db.MarketplaceListings.CountAsync(l => l.Status == "pending_review");
            var openDisputes = await db.Disputes.CountAsync(d => d.Status == "open");

            return Results.Ok(new
            {
                totalTenants, totalUsers, totalOrders,
                totalRevenueCents = totalRevenue,
                totalRevenueFormatted = $"${totalRevenue / 100.0:F2}",
                activeSubscriptions, pendingListings, openDisputes
            });
        }).WithName("AdminDashboard").WithTags("Admin");

        app.MapGet("/api/admin/tenants", [Authorize] async (HttpContext http, CreatorOsContext db, int? page, int? pageSize) =>
        {
            var p = page ?? 1;
            var ps = pageSize ?? 20;
            var tenants = await db.Tenants
                .OrderByDescending(t => t.CreatedAt)
                .Skip((p - 1) * ps).Take(ps)
                .Select(t => new { t.Id, t.Name, t.Slug, t.Plan, t.Status, t.CreatedAt })
                .ToListAsync();
            var total = await db.Tenants.CountAsync();
            return Results.Ok(new { tenants, total, page = p, pageSize = ps });
        }).WithName("AdminGetTenants").WithTags("Admin");

        app.MapPut("/api/admin/tenants/{id}/status", [Authorize] async (Guid id, AdminTenantStatusRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenant = await db.Tenants.FindAsync(id);
            if (tenant == null) return Results.NotFound();
            tenant.Status = req.Status;
            tenant.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { tenant.Id, tenant.Status });
        }).WithName("AdminUpdateTenantStatus").WithTags("Admin");

        app.MapGet("/api/admin/listings", [Authorize] async (HttpContext http, CreatorOsContext db, string? status) =>
        {
            var query = db.MarketplaceListings.AsQueryable();
            if (!string.IsNullOrEmpty(status)) query = query.Where(l => l.Status == status);
            var listings = await query.OrderByDescending(l => l.CreatedAt).Take(50)
                .Select(l => new { l.Id, l.Title, l.Status, l.TotalSales, l.CreatedAt })
                .ToListAsync();
            return Results.Ok(listings);
        }).WithName("AdminGetListings").WithTags("Admin");

        app.MapPost("/api/admin/disputes/{id}/resolve", [Authorize] async (Guid id, ResolveDisputeRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var dispute = await db.Disputes.FindAsync(id);
            if (dispute == null) return Results.NotFound();
            var userId = Guid.Parse(http.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            dispute.Status = "resolved";
            dispute.Resolution = req.Resolution;
            dispute.RefundAmountCents = req.RefundAmountCents;
            dispute.ResolvedBy = userId;
            dispute.ResolvedAt = DateTime.UtcNow;
            dispute.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { dispute.Id, dispute.Status });
        }).WithName("AdminResolveDispute").WithTags("Admin");

        app.MapGet("/api/admin/audit-logs", [Authorize] async (HttpContext http, CreatorOsContext db, int? page) =>
        {
            var p = page ?? 1;
            var logs = await db.SecurityAuditLogs
                .OrderByDescending(l => l.CreatedAt)
                .Skip((p - 1) * 50).Take(50)
                .Select(l => new { l.Id, l.UserId, l.Action, l.EntityType, l.EntityId, l.RiskLevel, l.CreatedAt })
                .ToListAsync();
            return Results.Ok(logs);
        }).WithName("AdminGetAuditLogs").WithTags("Admin");

        app.MapGet("/api/admin/analytics", [Authorize] async (HttpContext http, CreatorOsContext db, int? days) =>
        {
            var since = DateTime.UtcNow.AddDays(-(days ?? 30));
            var revenue = await db.Orders.Where(o => o.Status == "completed" && o.CreatedAt >= since)
                .GroupBy(o => o.CreatedAt.Date)
                .Select(g => new { date = g.Key, revenueCents = g.Sum(o => o.TotalCents), orders = g.Count() })
                .OrderBy(g => g.date).ToListAsync();

            var newUsers = await db.Users.CountAsync(u => u.CreatedAt >= since);
            var newTenants = await db.Tenants.CountAsync(t => t.CreatedAt >= since);
            var aiUsage = await db.AiAgentTasks.CountAsync(t => t.CreatedAt >= since);

            return Results.Ok(new { revenue, newUsers, newTenants, aiUsage });
        }).WithName("AdminAnalytics").WithTags("Admin");
    }
}

public record AdminTenantStatusRequest(string Status);
public record ResolveDisputeRequest(string Resolution, long RefundAmountCents);
