using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CreatorOS.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this WebApplication app)
    {
        app.MapGet("/api/products", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant selected" });

            var products = await db.Products
                .Where(p => p.TenantId == tenantId && p.IsActive)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new { p.Id, p.Name, p.ProductType, p.PriceCents, p.Currency, p.IsPublished, p.ThumbnailUrl })
                .ToListAsync();

            return Results.Ok(products);
        })
        .WithName("GetProducts")
        .WithTags("Products");

        app.MapGet("/api/products/{id}", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant selected" });

            var product = await db.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.TenantId == tenantId && p.IsActive);

            return product != null ? Results.Ok(product) : Results.NotFound();
        })
        .WithName("GetProduct")
        .WithTags("Products");

        app.MapPost("/api/products", [Authorize] async (CreateProductRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant selected" });

            var product = new Product
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId.Value,
                Name = req.Name,
                Description = req.Description,
                ProductType = req.ProductType,
                PriceCents = req.PriceCents,
                Currency = "USD",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            db.Products.Add(product);
            await db.SaveChangesAsync();

            return Results.Created($"/api/products/{product.Id}", new { product.Id, product.Name });
        })
        .WithName("CreateProduct")
        .WithTags("Products");

        app.MapPut("/api/products/{id}", [Authorize] async (Guid id, UpdateProductRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant selected" });

            var product = await db.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.TenantId == tenantId && p.IsActive);

            if (product == null) return Results.NotFound();

            product.Name = req.Name ?? product.Name;
            product.Description = req.Description ?? product.Description;
            product.PriceCents = req.PriceCents ?? product.PriceCents;
            product.UpdatedAt = DateTime.UtcNow;

            await db.SaveChangesAsync();
            return Results.Ok(new { product.Id, product.Name });
        })
        .WithName("UpdateProduct")
        .WithTags("Products");

        app.MapDelete("/api/products/{id}", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant selected" });

            var product = await db.Products
                .FirstOrDefaultAsync(p => p.Id == id && p.TenantId == tenantId && p.IsActive);

            if (product == null) return Results.NotFound();

            product.IsActive = false;
            product.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();

            return Results.Ok(new { message = "Product deleted" });
        })
        .WithName("DeleteProduct")
        .WithTags("Products");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CreateProductRequest(string Name, string? Description, string ProductType, long PriceCents);
public record UpdateProductRequest(string? Name, string? Description, long? PriceCents);
