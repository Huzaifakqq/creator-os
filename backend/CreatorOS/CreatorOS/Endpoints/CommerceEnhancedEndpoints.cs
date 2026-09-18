using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CreatorOS.Endpoints;

public static class CommerceEnhancedEndpoints
{
    public static void MapCommerceEnhancedEndpoints(this WebApplication app)
    {
        app.MapPost("/api/products/{id}/publish", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var product = await db.Products.FirstOrDefaultAsync(p => p.Id == id && p.TenantId == tenantId);
            if (product == null) return Results.NotFound();
            product.IsPublished = true;
            product.PublishedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { product.Id, message = "Product published" });
        }).WithName("PublishProduct").WithTags("Commerce");

        app.MapPost("/api/products/{id}/checkout", [Authorize] async (Guid id, CheckoutRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var product = await db.Products.FirstOrDefaultAsync(p => p.Id == id && p.TenantId == tenantId);
            if (product == null) return Results.NotFound();

            var session = new { sessionId = $"cs_dev_{Guid.NewGuid():N}", url = $"https://checkout.stripe.com/dev/{id}" };
            return Results.Ok(new { checkoutSession = session, message = "Stripe checkout session created (dev mode). Stripe integration pending." });
        }).WithName("Checkout").WithTags("Commerce");

        app.MapPost("/api/webhooks/stripe", async (HttpContext http, CreatorOsContext db) =>
        {
            using var reader = new StreamReader(http.Request.Body);
            var body = await reader.ReadToEndAsync();
            return Results.Ok(new { received = true, _note = "Webhook processing pending Stripe SDK integration" });
        }).WithName("StripeWebhook").WithTags("Commerce");

        app.MapGet("/api/payments", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var payments = await db.Payments.Where(p => p.TenantId == tenantId)
                .OrderByDescending(p => p.CreatedAt).Take(50)
                .Select(p => new { p.Id, p.OrderId, p.AmountCents, p.Currency, p.Status, p.PaymentMethod, p.CreatedAt })
                .ToListAsync();
            return Results.Ok(payments);
        }).WithName("GetPayments").WithTags("Commerce");

        app.MapPost("/api/orders/{id}/refund", [Authorize] async (Guid id, RefundRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var order = await db.Orders.FirstOrDefaultAsync(o => o.Id == id && o.TenantId == tenantId);
            if (order == null) return Results.NotFound();

            var refund = new Refund
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, OrderId = id,
                AmountCents = req.AmountCents ?? order.TotalCents, Reason = req.Reason,
                Status = "pending", CreatedAt = DateTime.UtcNow
            };
            db.Refunds.Add(refund);
            order.Status = "refunded";
            order.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Created($"/api/refunds/{refund.Id}", new { refund.Id, refund.AmountCents });
        }).WithName("RefundOrder").WithTags("Commerce");

        app.MapGet("/api/refunds", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var refunds = await db.Refunds.Where(r => r.TenantId == tenantId)
                .OrderByDescending(r => r.CreatedAt).Take(50)
                .Select(r => new { r.Id, r.OrderId, r.AmountCents, r.Reason, r.Status, r.CreatedAt })
                .ToListAsync();
            return Results.Ok(refunds);
        }).WithName("GetRefunds").WithTags("Commerce");

        app.MapGet("/api/invoices", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var invoices = await db.Invoices.Where(i => i.TenantId == tenantId)
                .OrderByDescending(i => i.CreatedAt).Take(50)
                .Select(i => new { i.Id, i.InvoiceNumber, i.CustomerEmail, i.TotalCents, i.Status, i.PaidAt, i.CreatedAt })
                .ToListAsync();
            return Results.Ok(invoices);
        }).WithName("GetInvoices").WithTags("Commerce");

        app.MapPost("/api/invoices", [Authorize] async (CreateInvoiceRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var count = await db.Invoices.CountAsync(i => i.TenantId == tenantId) + 1;
            var invoice = new Invoice
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value,
                InvoiceNumber = $"INV-{count:D6}", CustomerEmail = req.CustomerEmail,
                CustomerName = req.CustomerName, LineItems = req.LineItems ?? "[]",
                SubtotalCents = req.SubtotalCents, TaxCents = req.TaxCents,
                TotalCents = req.SubtotalCents + req.TaxCents, Currency = "USD",
                Status = "draft", CreatedAt = DateTime.UtcNow
            };
            db.Invoices.Add(invoice);
            await db.SaveChangesAsync();
            return Results.Created($"/api/invoices/{invoice.Id}", new { invoice.Id, invoice.InvoiceNumber });
        }).WithName("CreateInvoice").WithTags("Commerce");

        app.MapGet("/api/disputes", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var disputes = await db.Disputes.Where(d => d.TenantId == tenantId)
                .OrderByDescending(d => d.CreatedAt).Take(50)
                .Select(d => new { d.Id, d.OrderId, d.Reason, d.Status, d.RefundAmountCents, d.CreatedAt })
                .ToListAsync();
            return Results.Ok(disputes);
        }).WithName("GetDisputes").WithTags("Commerce");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CheckoutRequest(string? Email);
public record RefundRequest(long? AmountCents, string? Reason);
public record CreateInvoiceRequest(string? CustomerEmail, string? CustomerName, string? LineItems, long SubtotalCents, long TaxCents);
