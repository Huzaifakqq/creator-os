using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CreatorOS.Endpoints;

public static class AuditLogEndpoints
{
    public static void MapAuditLogEndpoints(this WebApplication app)
    {
        app.MapGet("/api/audit-logs", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var logs = await db.SecurityAuditLogs.Where(l => l.TenantId == tenantId)
                .OrderByDescending(l => l.CreatedAt).Take(100)
                .Select(l => new { l.Id, l.UserId, l.Action, l.EntityType, l.EntityId, l.IpAddress, l.RiskLevel, l.CreatedAt })
                .ToListAsync();
            return Results.Ok(logs);
        }).WithName("GetAuditLogs").WithTags("Audit");

        app.MapGet("/api/audit-logs/{id}", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var log = await db.SecurityAuditLogs.FirstOrDefaultAsync(l => l.Id == id && l.TenantId == tenantId);
            return log != null ? Results.Ok(log) : Results.NotFound();
        }).WithName("GetAuditLog").WithTags("Audit");

        app.MapGet("/api/domains", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var domains = await db.DomainSettings.Where(d => d.TenantId == tenantId)
                .Select(d => new { d.Id, d.Domain, d.IsCustom, d.SslStatus, d.IsVerified, d.AppId }).ToListAsync();
            return Results.Ok(domains);
        }).WithName("GetDomains").WithTags("Settings");

        app.MapPost("/api/domains", [Authorize] async (CreateDomainRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var domain = new DomainSetting
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, AppId = req.AppId,
                Domain = req.Domain, IsCustom = true, SslStatus = "pending",
                IsVerified = false, CreatedAt = DateTime.UtcNow
            };
            db.DomainSettings.Add(domain);
            await db.SaveChangesAsync();
            return Results.Created($"/api/domains/{domain.Id}", new { domain.Id, domain.Domain, domain.DnsVerificationToken });
        }).WithName("CreateDomain").WithTags("Settings");

        app.MapGet("/api/settings/platform", [Authorize] async (CreatorOsContext db) =>
        {
            var settings = await db.PlatformSettings.Select(s => new { s.Id, s.SettingKey, s.SettingValue, s.Description }).ToListAsync();
            return Results.Ok(settings);
        }).WithName("GetPlatformSettings").WithTags("Settings");

        app.MapPut("/api/settings/platform", [Authorize] async (UpdatePlatformSettingsRequest req, HttpContext http, CreatorOsContext db) =>
        {
            foreach (var setting in req.Settings)
            {
                var existing = await db.PlatformSettings.FirstOrDefaultAsync(s => s.SettingKey == setting.Key);
                if (existing != null) { existing.SettingValue = setting.Value; existing.UpdatedAt = DateTime.UtcNow; }
                else
                {
                    db.PlatformSettings.Add(new PlatformSetting
                    {
                        Id = Guid.NewGuid(), SettingKey = setting.Key, SettingValue = setting.Value,
                        Description = setting.Description, IsEncrypted = false, UpdatedAt = DateTime.UtcNow
                    });
                }
            }
            await db.SaveChangesAsync();
            return Results.Ok(new { message = "Settings updated" });
        }).WithName("UpdatePlatformSettings").WithTags("Settings");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CreateDomainRequest(string Domain, Guid? AppId);
public record UpdatePlatformSettingsRequest(List<SettingInput> Settings);
public record SettingInput(string Key, string Value, string? Description);
