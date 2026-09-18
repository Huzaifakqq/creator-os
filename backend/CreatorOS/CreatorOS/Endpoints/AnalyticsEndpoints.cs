using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Endpoints;

public static class AnalyticsEndpoints
{
    public static void MapAnalyticsEndpoints(this WebApplication app)
    {
        app.MapGet("/api/analytics/overview", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });

            var totalOrders = await db.Orders.CountAsync(o => o.TenantId == tenantId);
            var totalRevenue = await db.Orders
                .Where(o => o.TenantId == tenantId && o.Status == "completed")
                .SumAsync(o => o.TotalCents);
            var totalCustomers = await db.CrmContacts.CountAsync(c => c.TenantId == tenantId);
            var totalProducts = await db.Products.CountAsync(p => p.TenantId == tenantId);
            var totalApps = await db.Apps.CountAsync(a => a.TenantId == tenantId);

            var recentOrders = await db.Orders
                .Where(o => o.TenantId == tenantId)
                .OrderByDescending(o => o.CreatedAt).Take(5)
                .Select(o => new { o.Id, o.Status, o.TotalCents, o.CreatedAt })
                .ToListAsync();

            return Results.Ok(new
            {
                totalOrders,
                totalRevenueCents = totalRevenue,
                totalRevenueFormatted = $"${totalRevenue / 100.0:F2}",
                totalCustomers,
                totalProducts,
                totalApps,
                recentOrders
            });
        }).WithName("GetAnalyticsOverview").WithTags("Analytics");

        app.MapGet("/api/analytics/revenue", [Authorize] async (HttpContext http, CreatorOsContext db, int? days) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var since = DateTime.UtcNow.AddDays(-(days ?? 30));
            var revenue = await db.Orders
                .Where(o => o.TenantId == tenantId && o.Status == "completed" && o.CreatedAt >= since)
                .GroupBy(o => o.CreatedAt.Date)
                .Select(g => new { date = g.Key, revenueCents = g.Sum(o => o.TotalCents), orders = g.Count() })
                .OrderBy(g => g.date).ToListAsync();
            return Results.Ok(revenue);
        }).WithName("GetRevenueAnalytics").WithTags("Analytics");

        app.MapGet("/api/analytics/products", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var productStats = await db.OrderItems
                .Where(oi => oi.Order!.TenantId == tenantId && oi.Order.Status == "completed")
                .GroupBy(oi => oi.ProductId)
                .Select(g => new { productId = g.Key, unitsSold = g.Count(), revenueCents = g.Sum(oi => oi.TotalCents) })
                .OrderByDescending(x => x.revenueCents).ToListAsync();
            return Results.Ok(productStats);
        }).WithName("GetProductAnalytics").WithTags("Analytics");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}
