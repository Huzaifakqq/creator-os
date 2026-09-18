using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Endpoints;

public static class CouponEndpoints
{
    public static void MapCouponEndpoints(this WebApplication app)
    {
        app.MapGet("/api/coupons", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var coupons = await db.Coupons.Where(c => c.TenantId == tenantId)
                .Select(c => new { c.Id, c.Code, c.DiscountType, c.DiscountValue, c.UsageLimit, c.UsedCount, c.IsActive })
                .ToListAsync();
            return Results.Ok(coupons);
        }).WithName("GetCoupons").WithTags("Coupons");

        app.MapPost("/api/coupons", [Authorize] async (CreateCouponRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var coupon = new Coupon
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, Code = req.Code.ToUpper(),
                DiscountType = req.DiscountType, DiscountValue = req.DiscountValue,
                UsageLimit = req.UsageLimit, UsedCount = 0, PerUserLimit = 1,
                AppliesTo = "all", StartsAt = DateTime.UtcNow, IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            db.Coupons.Add(coupon);
            await db.SaveChangesAsync();
            return Results.Created($"/api/coupons/{coupon.Id}", new { coupon.Id, coupon.Code });
        }).WithName("CreateCoupon").WithTags("Coupons");

        app.MapPost("/api/coupons/validate", [Authorize] async (ValidateCouponRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var coupon = await db.Coupons.FirstOrDefaultAsync(c =>
                c.Code == req.Code.ToUpper() && c.TenantId == tenantId && c.IsActive);
            if (coupon == null) return Results.BadRequest(new { error = "Invalid coupon" });
            if (coupon.UsageLimit.HasValue && coupon.UsedCount >= coupon.UsageLimit.Value)
                return Results.BadRequest(new { error = "Coupon usage limit reached" });
            if (coupon.ExpiresAt.HasValue && coupon.ExpiresAt < DateTime.UtcNow)
                return Results.BadRequest(new { error = "Coupon expired" });
            return Results.Ok(new { coupon.Id, coupon.Code, coupon.DiscountType, coupon.DiscountValue });
        }).WithName("ValidateCoupon").WithTags("Coupons");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CreateCouponRequest(string Code, string DiscountType, long DiscountValue, int? UsageLimit);
public record ValidateCouponRequest(string Code);
