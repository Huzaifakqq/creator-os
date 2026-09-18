using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Endpoints;

public static class BuilderEndpoints
{
    public static void MapBuilderEndpoints(this WebApplication app)
    {
        app.MapPost("/api/builder/ai/generate", [Authorize] async (AiGenerateRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var userId = Guid.Parse(System.Security.Claims.ClaimTypes.NameIdentifier);
            userId = Guid.Parse(http.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var session = new AiGenerationSession
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, UserId = userId,
                Prompt = req.Prompt, Status = "processing", CreatedAt = DateTime.UtcNow
            };
            db.AiGenerationSessions.Add(session);
            await db.SaveChangesAsync();
            return Results.Ok(new { session.Id, message = "AI generation queued. OpenAI integration pending." });
        }).WithName("AiGenerate").WithTags("Builder");

        app.MapPost("/api/builder/ai/edit", [Authorize] async (AiEditRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var userId = Guid.Parse(http.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var session = new AiGenerationSession
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, UserId = userId,
                Prompt = $"Edit: {req.Instruction}", Status = "processing", CreatedAt = DateTime.UtcNow
            };
            db.AiGenerationSessions.Add(session);
            await db.SaveChangesAsync();
            return Results.Ok(new { session.Id, message = "AI edit queued." });
        }).WithName("AiEdit").WithTags("Builder");

        app.MapPost("/api/sites/{appId}/publish", [Authorize] async (Guid appId, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var app_ = await db.Apps.FirstOrDefaultAsync(a => a.Id == appId && a.TenantId == tenantId);
            if (app_ == null) return Results.NotFound();
            var latestVersion = await db.AppVersions.Where(v => v.AppId == appId).OrderByDescending(v => v.VersionNumber).FirstOrDefaultAsync();
            var version = latestVersion != null ? latestVersion.VersionNumber + 1 : 1;
            var newVersion = new AppVersion
            {
                Id = Guid.NewGuid(), AppId = appId, VersionNumber = version,
                Config = app_.Config ?? "{}", CreatedAt = DateTime.UtcNow
            };
            db.AppVersions.Add(newVersion);
            var deployment = new AppDeployment
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, AppId = appId,
                VersionId = newVersion.Id, Status = "deployed", DeployedAt = DateTime.UtcNow, CreatedAt = DateTime.UtcNow
            };
            db.AppDeployments.Add(deployment);
            app_.PublishedVersion = version;
            app_.Status = "published";
            app_.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { version, message = "Site published successfully" });
        }).WithName("PublishSite").WithTags("Builder");

        app.MapGet("/api/apps/{appId}/versions", [Authorize] async (Guid appId, HttpContext http, CreatorOsContext db) =>
        {
            var versions = await db.AppVersions.Where(v => v.AppId == appId)
                .OrderByDescending(v => v.VersionNumber).Take(20)
                .Select(v => new { v.Id, v.VersionNumber, v.Changelog, v.CreatedAt }).ToListAsync();
            return Results.Ok(versions);
        }).WithName("GetAppVersions").WithTags("Builder");

        app.MapPost("/api/apps/{appId}/rollback", [Authorize] async (Guid appId, RollbackRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var app_ = await db.Apps.FirstOrDefaultAsync(a => a.Id == appId && a.TenantId == tenantId);
            if (app_ == null) return Results.NotFound();
            var targetVersion = await db.AppVersions.FirstOrDefaultAsync(v => v.AppId == appId && v.VersionNumber == req.Version);
            if (targetVersion == null) return Results.BadRequest(new { error = "Version not found" });
            app_.Config = targetVersion.Config;
            app_.PublishedVersion = req.Version;
            app_.Status = "published";
            app_.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { message = $"Rolled back to version {req.Version}" });
        }).WithName("RollbackApp").WithTags("Builder");

        app.MapGet("/api/apps/{appId}/deployments", [Authorize] async (Guid appId, HttpContext http, CreatorOsContext db) =>
        {
            var deployments = await db.AppDeployments.Where(d => d.AppId == appId)
                .OrderByDescending(d => d.CreatedAt).Take(10)
                .Select(d => new { d.Id, d.Status, d.DeploymentUrl, d.DeployedAt, d.RolledBackAt }).ToListAsync();
            return Results.Ok(deployments);
        }).WithName("GetDeployments").WithTags("Builder");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record AiGenerateRequest(string Prompt, string? Type);
public record AiEditRequest(string Instruction, string ExistingConfig);
public record RollbackRequest(int Version);
