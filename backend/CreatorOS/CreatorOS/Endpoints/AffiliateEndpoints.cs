using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CreatorOS.Endpoints;

public static class AffiliateEndpoints
{
    public static void MapAffiliateEndpoints(this WebApplication app)
    {
        app.MapGet("/api/affiliates/programs", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var programs = await db.AffiliatePrograms.Where(p => p.TenantId == tenantId)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new { p.Id, p.Name, p.CommissionType, p.CommissionValue, p.CookieDays, p.Status })
                .ToListAsync();
            return Results.Ok(programs);
        }).WithName("GetAffiliatePrograms").WithTags("Affiliates");

        app.MapPost("/api/affiliates/programs", [Authorize] async (CreateProgramRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var program = new AffiliateProgram
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, Name = req.Name,
                Description = req.Description, CommissionType = req.CommissionType ?? "percentage",
                CommissionValue = req.CommissionValue, CookieDays = req.CookieDays ?? 30,
                MinimumPayoutCents = req.MinimumPayoutCents ?? 5000, Status = "active",
                CreatedAt = DateTime.UtcNow
            };
            db.AffiliatePrograms.Add(program);
            await db.SaveChangesAsync();
            return Results.Created($"/api/affiliates/programs/{program.Id}", new { program.Id, program.Name });
        }).WithName("CreateAffiliateProgram").WithTags("Affiliates");

        app.MapGet("/api/affiliates/programs/{programId}/members", [Authorize] async (Guid programId, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var members = await db.AffiliateMembers.Where(m => m.ProgramId == programId && m.TenantId == tenantId)
                .Select(m => new { m.Id, m.AffiliateId, m.Tier, m.TotalEarningsCents, m.PendingPayoutCents, m.TotalReferrals, m.TotalConversions, m.Status })
                .ToListAsync();
            return Results.Ok(members);
        }).WithName("GetAffiliateMembers").WithTags("Affiliates");

        app.MapPost("/api/affiliates/programs/{programId}/join", [Authorize] async (Guid programId, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var userId = Guid.Parse(http.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var existing = await db.AffiliateMembers.FirstOrDefaultAsync(m => m.ProgramId == programId && m.AffiliateId == userId);
            if (existing != null) return Results.BadRequest(new { error = "Already a member" });
            var member = new AffiliateMember
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, ProgramId = programId,
                AffiliateId = userId, Tier = "standard", TotalEarningsCents = 0,
                PendingPayoutCents = 0, TotalReferrals = 0, TotalConversions = 0,
                Status = "active", CreatedAt = DateTime.UtcNow
            };
            db.AffiliateMembers.Add(member);
            await db.SaveChangesAsync();
            return Results.Created($"/api/affiliates/members/{member.Id}", new { member.Id });
        }).WithName("JoinAffiliateProgram").WithTags("Affiliates");

        app.MapGet("/api/affiliates/links", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var userId = Guid.Parse(http.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var member = await db.AffiliateMembers.FirstOrDefaultAsync(m => m.AffiliateId == userId && m.TenantId == tenantId);
            if (member == null) return Results.BadRequest(new { error = "Not an affiliate" });
            var links = await db.AffiliateLinks.Where(l => l.MemberId == member.Id)
                .Select(l => new { l.Id, l.Code, l.TargetUrl, l.ClickCount, l.ConversionCount, l.RevenueCents, l.IsActive })
                .ToListAsync();
            return Results.Ok(links);
        }).WithName("GetAffiliateLinks").WithTags("Affiliates");

        app.MapPost("/api/affiliates/links", [Authorize] async (CreateLinkRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var userId = Guid.Parse(http.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var member = await db.AffiliateMembers.FirstOrDefaultAsync(m => m.AffiliateId == userId && m.TenantId == tenantId);
            if (member == null) return Results.BadRequest(new { error = "Not an affiliate" });
            var code = Guid.NewGuid().ToString("N")[..8];
            var link = new AffiliateLink
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, MemberId = member.Id,
                Code = code, TargetUrl = req.TargetUrl, ClickCount = 0,
                ConversionCount = 0, RevenueCents = 0, IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            db.AffiliateLinks.Add(link);
            await db.SaveChangesAsync();
            return Results.Created($"/api/affiliates/links/{link.Id}", new { link.Id, link.Code });
        }).WithName("CreateAffiliateLink").WithTags("Affiliates");

        app.MapGet("/api/affiliates/conversions", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var userId = Guid.Parse(http.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var member = await db.AffiliateMembers.FirstOrDefaultAsync(m => m.AffiliateId == userId && m.TenantId == tenantId);
            if (member == null) return Results.BadRequest(new { error = "Not an affiliate" });
            var conversions = await db.AffiliateConversions.Where(c => c.Link!.MemberId == member.Id)
                .OrderByDescending(c => c.CreatedAt).Take(50)
                .Select(c => new { c.Id, c.OrderId, c.CommissionCents, c.Status, c.CreatedAt })
                .ToListAsync();
            return Results.Ok(conversions);
        }).WithName("GetAffiliateConversions").WithTags("Affiliates");

        app.MapGet("/api/affiliates/payouts", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var userId = Guid.Parse(http.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var member = await db.AffiliateMembers.FirstOrDefaultAsync(m => m.AffiliateId == userId && m.TenantId == tenantId);
            if (member == null) return Results.BadRequest(new { error = "Not an affiliate" });
            var payouts = await db.AffiliatePayouts.Where(p => p.MemberId == member.Id)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new { p.Id, p.AmountCents, p.PayoutMethod, p.Status, p.PeriodStart, p.PeriodEnd, p.ProcessedAt })
                .ToListAsync();
            return Results.Ok(payouts);
        }).WithName("GetAffiliatePayouts").WithTags("Affiliates");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CreateProgramRequest(string Name, string? Description, string? CommissionType, decimal CommissionValue, int? CookieDays, long? MinimumPayoutCents);
public record CreateLinkRequest(string TargetUrl);
