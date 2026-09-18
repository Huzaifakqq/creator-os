using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CreatorOS.Endpoints;

public static class ContactEndpoints
{
    public static void MapContactEndpoints(this WebApplication app)
    {
        app.MapGet("/api/contacts", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var contacts = await db.CrmContacts.Where(c => c.TenantId == tenantId)
                .OrderByDescending(c => c.CreatedAt).Take(100)
                .Select(c => new { c.Id, c.Email, c.FirstName, c.LastName, c.Status, c.Score, c.LifetimeValueCents, c.CreatedAt })
                .ToListAsync();
            return Results.Ok(contacts);
        }).WithName("GetContacts").WithTags("Contacts");

        app.MapGet("/api/contacts/{id}", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var contact = await db.CrmContacts.FirstOrDefaultAsync(c => c.Id == id && c.TenantId == tenantId);
            return contact != null ? Results.Ok(contact) : Results.NotFound();
        }).WithName("GetContact").WithTags("Contacts");

        app.MapPost("/api/contacts", [Authorize] async (CreateContactRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var contact = new CrmContact
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, Email = req.Email,
                FirstName = req.FirstName, LastName = req.LastName,
                ContactType = "lead", Status = "active", Source = "manual",
                Score = 0, LifetimeValueCents = 0, CustomFields = "{}",
                CreatedAt = DateTime.UtcNow
            };
            db.CrmContacts.Add(contact);
            await db.SaveChangesAsync();
            return Results.Created($"/api/contacts/{contact.Id}", new { contact.Id, contact.Email });
        }).WithName("CreateContact").WithTags("Contacts");

        app.MapPut("/api/contacts/{id}", [Authorize] async (Guid id, UpdateContactRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var contact = await db.CrmContacts.FirstOrDefaultAsync(c => c.Id == id && c.TenantId == tenantId);
            if (contact == null) return Results.NotFound();
            if (req.FirstName != null) contact.FirstName = req.FirstName;
            if (req.LastName != null) contact.LastName = req.LastName;
            if (req.Status != null) contact.Status = req.Status;
            contact.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { contact.Id, contact.Email });
        }).WithName("UpdateContact").WithTags("Contacts");

        app.MapDelete("/api/contacts/{id}", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var contact = await db.CrmContacts.FirstOrDefaultAsync(c => c.Id == id && c.TenantId == tenantId);
            if (contact == null) return Results.NotFound();
            db.CrmContacts.Remove(contact);
            await db.SaveChangesAsync();
            return Results.Ok(new { message = "Contact deleted" });
        }).WithName("DeleteContact").WithTags("Contacts");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record CreateContactRequest(string Email, string? FirstName, string? LastName);
public record UpdateContactRequest(string? FirstName, string? LastName, string? Status);
