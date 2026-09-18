using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Endpoints;

public static class EmailEnhancedEndpoints
{
    public static void MapEmailEnhancedEndpoints(this WebApplication app)
    {
        app.MapGet("/api/email/lists", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var lists = await db.EmailLists.Where(l => l.TenantId == tenantId)
                .OrderByDescending(l => l.CreatedAt)
                .Select(l => new { l.Id, l.Name, l.Description, l.SubscriberCount, l.IsActive }).ToListAsync();
            return Results.Ok(lists);
        }).WithName("GetEmailLists").WithTags("Email");

        app.MapPost("/api/email/lists", [Authorize] async (CreateEmailListRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var list = new EmailList
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, Name = req.Name,
                Description = req.Description, SubscriberCount = 0, IsActive = true, CreatedAt = DateTime.UtcNow
            };
            db.EmailLists.Add(list);
            await db.SaveChangesAsync();
            return Results.Created($"/api/email/lists/{list.Id}", new { list.Id, list.Name });
        }).WithName("CreateEmailList").WithTags("Email");

        app.MapPost("/api/email/lists/{listId}/subscribers", [Authorize] async (Guid listId, AddSubscriberRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var subscriber = new EmailSubscriber
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, ListId = listId,
                Email = req.Email, Status = "active", SubscribedAt = DateTime.UtcNow, CreatedAt = DateTime.UtcNow
            };
            db.EmailSubscribers.Add(subscriber);
            var list = await db.EmailLists.FindAsync(listId);
            if (list != null) list.SubscriberCount += 1;
            await db.SaveChangesAsync();
            return Results.Ok(new { subscriber.Id, subscriber.Email });
        }).WithName("AddSubscriber").WithTags("Email");

        app.MapGet("/api/email/sequences", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var sequences = await db.EmailSequences.Where(s => s.TenantId == tenantId)
                .OrderByDescending(s => s.CreatedAt)
                .Select(s => new { s.Id, s.Name, s.TriggerType, s.Status, s.EnrolledCount, s.CompletedCount }).ToListAsync();
            return Results.Ok(sequences);
        }).WithName("GetEmailSequences").WithTags("Email");

        app.MapPost("/api/email/sequences", [Authorize] async (CreateSequenceRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var sequence = new EmailSequence
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, Name = req.Name,
                Description = req.Description, TriggerType = req.TriggerType ?? "manual",
                TriggerConfig = req.TriggerConfig, Steps = req.Steps ?? "[]",
                Status = "draft", EnrolledCount = 0, CompletedCount = 0, CreatedAt = DateTime.UtcNow
            };
            db.EmailSequences.Add(sequence);
            await db.SaveChangesAsync();
            return Results.Created($"/api/email/sequences/{sequence.Id}", new { sequence.Id, sequence.Name });
        }).WithName("CreateEmailSequence").WithTags("Email");

        app.MapPost("/api/email/sequences/{sequenceId}/enroll", [Authorize] async (Guid sequenceId, EnrollContactRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var enrollment = new EmailSequenceEnrollment
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, SequenceId = sequenceId,
                ContactId = req.ContactId, CurrentStep = 0, Status = "active",
                NextSendAt = DateTime.UtcNow, CreatedAt = DateTime.UtcNow
            };
            db.EmailSequenceEnrollments.Add(enrollment);
            var sequence = await db.EmailSequences.FindAsync(sequenceId);
            if (sequence != null) sequence.EnrolledCount += 1;
            await db.SaveChangesAsync();
            return Results.Ok(new { enrollment.Id });
        }).WithName("EnrollContactInSequence").WithTags("Email");

        app.MapGet("/api/email/events", [Authorize] async (HttpContext http, CreatorOsContext db, Guid? campaignId) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var query = db.EmailEvents.Where(e => e.TenantId == tenantId).AsQueryable();
            if (campaignId.HasValue) query = query.Where(e => e.CampaignId == campaignId);
            var events = await query.OrderByDescending(e => e.CreatedAt).Take(100)
                .Select(e => new { e.Id, e.CampaignId, e.SubscriberId, e.EventType, e.Metadata, e.CreatedAt }).ToListAsync();
            return Results.Ok(events);
        }).WithName("GetEmailEvents").WithTags("Email");

        app.MapGet("/api/email/templates", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var templates = await db.EmailTemplates.Where(t => t.TenantId == tenantId)
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new { t.Id, t.Name, t.Category, t.UsageCount, t.CreatedAt }).ToListAsync();
            return Results.Ok(templates);
        }).WithName("GetEmailTemplates").WithTags("Email");

        app.MapPost("/api/email/templates", [Authorize] async (CreateTemplateRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var template = new EmailTemplate
            {
                Id = Guid.NewGuid(), TenantId = tenantId, Name = req.Name,
                Category = req.Category ?? "general", HtmlContent = req.HtmlContent, CreatedAt = DateTime.UtcNow
            };
            db.EmailTemplates.Add(template);
            await db.SaveChangesAsync();
            return Results.Created($"/api/email/templates/{template.Id}", new { template.Id, template.Name });
        }).WithName("CreateEmailTemplate").WithTags("Email");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CreateEmailListRequest(string Name, string? Description);
public record AddSubscriberRequest(string Email);
public record CreateSequenceRequest(string Name, string? Description, string? TriggerType, string? TriggerConfig, string? Steps);
public record EnrollContactRequest(Guid ContactId);
public record CreateTemplateRequest(string Name, string? Category, string? HtmlContent);
