using CreatorOS.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CreatorOS.Endpoints;

public static class TeamEndpoints
{
    public static void MapTeamEndpoints(this WebApplication app)
    {
        app.MapGet("/api/teams/members", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var members = await db.TeamMembers.Where(m => m.TenantId == tenantId)
                .Select(m => new { m.Id, m.UserId, m.Role, m.Status, m.JoinedAt, m.CreatedAt })
                .ToListAsync();
            return Results.Ok(members);
        }).WithName("GetTeamMembers").WithTags("Teams");

        app.MapPost("/api/teams/invite", [Authorize] async (InviteMemberRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var userId = Guid.Parse(http.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var existing = await db.TeamMembers.FirstOrDefaultAsync(m => m.TenantId == tenantId && m.UserId == null);
            var existingInvite = await db.TeamInvites.FirstOrDefaultAsync(i => i.TenantId == tenantId && i.Email == req.Email && i.AcceptedAt == null);
            if (existingInvite != null) return Results.BadRequest(new { error = "Already invited" });

            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            var invite = new TeamInvite
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, Email = req.Email,
                Role = req.Role ?? "member", Token = token, InvitedBy = userId,
                ExpiresAt = DateTime.UtcNow.AddDays(7), CreatedAt = DateTime.UtcNow
            };
            db.TeamInvites.Add(invite);
            await db.SaveChangesAsync();

            return Results.Created($"/api/teams/invites/{invite.Id}", new { invite.Id, invite.Email, invite.Role, _dev_token = token });
        }).WithName("InviteTeamMember").WithTags("Teams");

        app.MapGet("/api/teams/invites", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var invites = await db.TeamInvites.Where(i => i.TenantId == tenantId && i.AcceptedAt == null)
                .OrderByDescending(i => i.CreatedAt)
                .Select(i => new { i.Id, i.Email, i.Role, i.ExpiresAt, i.CreatedAt })
                .ToListAsync();
            return Results.Ok(invites);
        }).WithName("GetTeamInvites").WithTags("Teams");

        app.MapPost("/api/teams/accept/{token}", async (string token, CreatorOsContext db) =>
        {
            var invite = await db.TeamInvites.FirstOrDefaultAsync(i => i.Token == token && i.AcceptedAt == null && i.ExpiresAt > DateTime.UtcNow);
            if (invite == null) return Results.BadRequest(new { error = "Invalid or expired invite" });
            invite.AcceptedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { message = "Invite accepted. Register/login to join the team." });
        }).WithName("AcceptInvite").WithTags("Teams");

        app.MapDelete("/api/teams/members/{id}", [Authorize] async (Guid id, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var member = await db.TeamMembers.FirstOrDefaultAsync(m => m.Id == id && m.TenantId == tenantId);
            if (member == null) return Results.NotFound();
            db.TeamMembers.Remove(member);
            await db.SaveChangesAsync();
            return Results.Ok(new { message = "Member removed" });
        }).WithName("RemoveTeamMember").WithTags("Teams");

        app.MapGet("/api/roles", [Authorize] async (HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var roles = await db.Roles.Where(r => r.TenantId == tenantId)
                .Select(r => new { r.Id, r.Name, r.Description, r.Permissions, r.IsSystem })
                .ToListAsync();
            return Results.Ok(roles);
        }).WithName("GetRoles").WithTags("Teams");

        app.MapPost("/api/roles", [Authorize] async (CreateRoleRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var role = new Role
            {
                Id = Guid.NewGuid(), TenantId = tenantId.Value, Name = req.Name,
                Description = req.Description, Permissions = req.Permissions ?? "[]",
                IsSystem = false, CreatedAt = DateTime.UtcNow
            };
            db.Roles.Add(role);
            await db.SaveChangesAsync();
            return Results.Created($"/api/roles/{role.Id}", new { role.Id, role.Name });
        }).WithName("CreateRole").WithTags("Teams");

        app.MapPut("/api/teams/members/{id}/role", [Authorize] async (Guid id, UpdateRoleRequest req, HttpContext http, CreatorOsContext db) =>
        {
            var tenantId = GetTenantId(http);
            if (tenantId == null) return Results.BadRequest(new { error = "No tenant" });
            var member = await db.TeamMembers.FirstOrDefaultAsync(m => m.Id == id && m.TenantId == tenantId);
            if (member == null) return Results.NotFound();
            member.Role = req.Role;
            member.UpdatedAt = DateTime.UtcNow;
            await db.SaveChangesAsync();
            return Results.Ok(new { member.Id, member.Role });
        }).WithName("UpdateMemberRole").WithTags("Teams");
    }

    private static Guid? GetTenantId(HttpContext http)
    {
        if (http.Items["TenantId"] is Guid tenantId) return tenantId;
        var header = http.Request.Headers["X-Tenant-Id"].FirstOrDefault();
        return Guid.TryParse(header, out var id) ? id : null;
    }
}

public record InviteMemberRequest(string Email, string? Role);
public record CreateRoleRequest(string Name, string? Description, string? Permissions);
public record UpdateRoleRequest(string Role);
