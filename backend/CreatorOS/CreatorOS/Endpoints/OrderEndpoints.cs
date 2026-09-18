using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CreatorOS.Endpoints;

public static class sOrderEndpoints
{
    public static void MapOrderEndpoints(this WebApplication app)
    {
        app.MapGet("/api/orders", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant selected" });

            var orders = await db.Orders
                .Where(o => o.TenantId == tenantId)
                .OrderByDescending(o => o.CreatedAt)
                .Select(o => new { o.Id, o.Status, o.TotalCents, o.Currency, o.Source, o.CreatedAt })
                .ToListAsync();

            return Results.Ok(orders);
        })
        .WithName("GetOrders")
        .WithTags("Orders");

        app.MapGet("/api/orders/{id}", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant selected" });

            var order = await db.Orders
                .Where(o => o.Id == id && o.TenantId == tenantId)
                .Select(o => new
                {
                    o.Id, o.Status, o.TotalCents, o.Currency, o.Source,
                    o.CreatedAt, o.CompletedAt,
                    Items = db.OrderItems
                        .Where(i => i.OrderId == o.Id)
                        .Select(i => new { i.Id, i.ProductId, i.Quantity, i.UnitPriceCents, i.TotalCents })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return order != null ? Results.Ok(order) : Results.NotFound();
        })
        .WithName("GetOrder")
        .WithTags("Orders");

        app.MapPost("/api/orders", [Authorize] async (CreateOrderRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant selected" });

            var order = new Order
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId.Value,
                CustomerId = req.CustomerId,
                Status = "pending",
                TotalCents = req.TotalCents,
                Currency = "USD",
                Source = "storefront",
                CreatedAt = DateTime.UtcNow
            };

            db.Orders.Add(order);

            foreach (var item in req.Items)
            {
                db.OrderItems.Add(new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPriceCents = item.UnitPriceCents,
                    TotalCents = item.Quantity * item.UnitPriceCents
                });
            }

            await db.SaveChangesAsync();
            return Results.Created($"/api/orders/{order.Id}", new { order.Id, order.Status });
        })
        .WithName("CreateOrder")
        .WithTags("Orders");

        app.MapPut("/api/orders/{id}/status", [Authorize] async (Guid id, UpdateOrderStatusRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant selected" });

            var order = await db.Orders
                .FirstOrDefaultAsync(o => o.Id == id && o.TenantId == tenantId);

            if (order == null) return Results.NotFound();

            order.Status = req.Status;
            if (req.Status == "completed") order.CompletedAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();
            return Results.Ok(new { order.Id, order.Status });
        })
        .WithName("UpdateOrderStatus")
        .WithTags("Orders");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CreateOrderRequest(Guid? CustomerId, long TotalCents, List<OrderItemRequest> Items);
public record OrderItemRequest(Guid ProductId, int Quantity, long UnitPriceCents);
public record UpdateOrderStatusRequest(string Status);
