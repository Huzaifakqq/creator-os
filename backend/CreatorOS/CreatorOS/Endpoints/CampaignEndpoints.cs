using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Endpoints;

public static class CampaignEndpoints
{
    public static void MapCampaignEndpoints(this WebApplication app)
    {
        app.MapGet("/api/campaigns", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var campaigns = await db.EmailCampaigns.Where(c => c.TenantId == tenantId)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new { c.Id, c.Name, c.Status, c.CampaignType, c.TotalSent, c.TotalOpened, c.TotalClicked, c.CreatedAt })
                .ToListAsync();
            return Results.Ok(campaigns);
        }).WithName("GetCampaigns").WithTags("Campaigns");

        app.MapPost("/api/campaigns", [Authorize] async (CreateCampaignRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var campaign = new EmailCampaign
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, Name = req.Name,
                Subject = req.Subject, HtmlContent = req.HtmlContent,
                CampaignType = "email", Status = "draft", ListId = req.ListId,
                TotalSent = 0, TotalOpened = 0, TotalClicked = 0,
                TotalBounced = 0, TotalUnsubscribed = 0, CreatedAt = DateTime.UtcNow
            };
            db.EmailCampaigns.Add(campaign);
            await db.SaveChangesAsync();
            return Results.Created($"/api/campaigns/{campaign.Id}", new { campaign.Id, campaign.Name });
        }).WithName("CreateCampaign").WithTags("Campaigns");

        app.MapPost("/api/campaigns/{id}/send", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var campaign = await db.EmailCampaigns.FirstOrDefaultAsync(c => c.Id == id && c.TenantId == tenantId);
            if (campaign == null) return Results.NotFound();
            campaign.Status = "sending";
            campaign.ScheduledAt = DateTime.UtcNow;
            campaign.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { campaign.Id, message = "Campaign queued. Email dispatch pending provider integration." });
        }).WithName("SendCampaign").WithTags("Campaigns");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CreateCampaignRequest(string Name, string? Subject, string? HtmlContent, Guid? ListId);
