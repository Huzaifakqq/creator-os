using CreatorOS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CreatorOS.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        app.MapPost("/api/auth/register", async (RegisterRequest req, AuthService authService) =>
        {
            Console.WriteLine($"[AUTH] Register attempt: {req.Email} role={req.Role}");
            try
            {
                var result = await authService.RegisterAsync(req.Email, req.Password, req.DisplayName, req.Role, req.TenantName);
                if (result.Success)
                    Console.WriteLine($"[AUTH] Register SUCCESS: {req.Email}");
                else
                    Console.WriteLine($"[AUTH] Register FAILED: {req.Email} - {result.Error}");
                return result.Success ? Results.Ok(result) : Results.BadRequest(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AUTH] Register EXCEPTION: {req.Email} - {ex.Message}");
                return Results.BadRequest(new { Success = false, Error = ex.Message });
            }
        })
        .WithName("Register")
        .WithTags("Auth");

        app.MapPost("/api/auth/login", async (LoginRequest req, AuthService authService) =>
        {
            Console.WriteLine($"[AUTH] Login attempt: {req.Email}");
            try
            {
                var result = await authService.LoginAsync(req.Email, req.Password);
                if (result.Success)
                    Console.WriteLine($"[AUTH] Login SUCCESS: {req.Email} tenants={result.Tenants?.Count ?? 0}");
                else
                    Console.WriteLine($"[AUTH] Login FAILED: {req.Email} - {result.Error}");
                return result.Success ? Results.Ok(result) : Results.Unauthorized();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AUTH] Login EXCEPTION: {req.Email} - {ex.Message}");
                return Results.BadRequest(new { Success = false, Error = ex.Message });
            }
        })
        .WithName("Login")
        .WithTags("Auth");

        app.MapGet("/api/auth/me", [Authorize] async (HttpContext http, CreatorOS.Domain.CreatorOsContext db) =>
        {
            var userId = http.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                Console.WriteLine("[AUTH] /me FAILED: No userId in token");
                return Results.Unauthorized();
            }

            var user = await db.Users.FindAsync(Guid.Parse(userId));
            if (user == null)
            {
                Console.WriteLine($"[AUTH] /me FAILED: User not found for {userId}");
                return Results.NotFound();
            }

            Console.WriteLine($"[AUTH] /me OK: {user.Email}");
            return Results.Ok(new { user.Id, user.Email, user.DisplayName });
        })
        .WithName("GetMe")
        .WithTags("Auth");

        app.MapPost("/api/auth/select-tenant", [Authorize] async (SelectTenantRequest req, HttpContext http, AuthService authService) =>
        {
            var userId = http.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Results.Unauthorized();

            var userGuid = Guid.Parse(userId);
            var db = http.RequestServices.GetRequiredService<CreatorOS.Domain.CreatorOsContext>();

            var membership = await db.UserTenantMemberships
                .FirstOrDefaultAsync(m => m.UserId == userGuid && m.TenantId == req.TenantId && m.Status == "active");

            if (membership == null) return Results.Forbid();

            var email = http.User.FindFirst(ClaimTypes.Email)?.Value ?? "";
            var token = authService.GenerateJwtTokenWithTenant(userGuid, email, req.TenantId, membership.Role);

            return Results.Ok(new { Success = true, Token = token, TenantId = req.TenantId, Role = membership.Role });
        })
        .WithName("SelectTenant")
        .WithTags("Auth")
        .RequireAuthorization();

        app.MapPost("/api/auth/join-workspace", [Authorize] async (JoinWorkspaceRequest req, HttpContext http, AuthService authService) =>
        {
            Console.WriteLine($"[AUTH] Join workspace attempt: {req.WorkspaceSlug}");
            var userId = http.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Results.Unauthorized();

            var userGuid = Guid.Parse(userId);
            var db = http.RequestServices.GetRequiredService<CreatorOS.Domain.CreatorOsContext>();

            var tenant = await db.Tenants.FirstOrDefaultAsync(t => t.Slug == req.WorkspaceSlug && t.Status == "active");
            if (tenant == null)
                return Results.BadRequest(new { Success = false, Error = "Workspace not found" });

            var existingMembership = await db.UserTenantMemberships
                .FirstOrDefaultAsync(m => m.UserId == userGuid && m.TenantId == tenant.Id);
            if (existingMembership != null)
                return Results.BadRequest(new { Success = false, Error = "You are already a member of this workspace" });

            var membership = new CreatorOS.Domain.UserTenantMembership
            {
                Id = Guid.NewGuid(),
                UserId = userGuid,
                TenantId = tenant.Id,
                Role = "member",
                Status = "active",
                JoinedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            db.UserTenantMemberships.Add(membership);
            await db.SaveChangesAsync();

            var email = http.User.FindFirst(ClaimTypes.Email)?.Value ?? "";
            var token = authService.GenerateJwtTokenWithTenant(userGuid, email, tenant.Id, "member");

            Console.WriteLine($"[AUTH] Join workspace SUCCESS: {req.WorkspaceSlug} for {email}");
            return Results.Ok(new { Success = true, Token = token, TenantId = tenant.Id, Role = "member", TenantName = tenant.Name });
        })
        .WithName("JoinWorkspace")
        .WithTags("Auth")
        .RequireAuthorization();
    }
}

public record RegisterRequest(string Email, string Password, string DisplayName, string Role, string? TenantName);
public record LoginRequest(string Email, string Password);
public record SelectTenantRequest(Guid TenantId);
public record JoinWorkspaceRequest(string WorkspaceSlug);
