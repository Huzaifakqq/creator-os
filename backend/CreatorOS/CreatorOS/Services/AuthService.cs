using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CreatorOS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CreatorOS.Services;

public class AuthService
{
    private readonly CreatorOsContext _db;
    private readonly IConfiguration _config;

    public AuthService(CreatorOsContext db, IConfiguration config)
    {
        _db = db;
        _config = config;
    }

    public async Task<AuthResponse> RegisterAsync(string email, string password, string displayName, string role, string? tenantName)
    {
        if (await _db.Users.AnyAsync(x => x.Email == email))
            return new AuthResponse(false, null, null, "Email already exists", null, false);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            DisplayName = displayName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            EmailConfirmed = false,
            OnboardingCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _db.Users.Add(user);

        if (role == "creator")
        {
            if (string.IsNullOrWhiteSpace(tenantName))
                return new AuthResponse(false, null, null, "Workspace name is required for creators", null, false);

            var slug = tenantName.ToLower().Replace(" ", "-").Replace("'", "");
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9-]", "");
            slug = slug.Trim('-');
            if (string.IsNullOrEmpty(slug)) slug = "workspace-" + Guid.NewGuid().ToString("N")[..8];

            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                Name = tenantName,
                Slug = slug,
                Plan = "free",
                Status = "active",
                ContactEmail = email,
                CreatedAt = DateTime.UtcNow
            };

            _db.Tenants.Add(tenant);

            var membership = new UserTenantMembership
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                TenantId = tenant.Id,
                Role = "owner",
                Status = "active",
                JoinedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            _db.UserTenantMemberships.Add(membership);

            var defaultRoles = new List<Role>
            {
                new Role { Id = Guid.NewGuid(), TenantId = tenant.Id, Name = "Owner", Description = "Full access to everything", Permissions = "*", IsSystem = true, CreatedAt = DateTime.UtcNow },
                new Role { Id = Guid.NewGuid(), TenantId = tenant.Id, Name = "Admin", Description = "Can manage most features", Permissions = "products,courses,members,agents,crm,email,analytics", IsSystem = true, CreatedAt = DateTime.UtcNow },
                new Role { Id = Guid.NewGuid(), TenantId = tenant.Id, Name = "Member", Description = "Can view and consume content", Permissions = "view_courses,view_memberships,view_coaching,orders", IsSystem = true, CreatedAt = DateTime.UtcNow },
                new Role { Id = Guid.NewGuid(), TenantId = tenant.Id, Name = "Viewer", Description = "Read-only access", Permissions = "view", IsSystem = true, CreatedAt = DateTime.UtcNow }
            };

            _db.Roles.AddRange(defaultRoles);
            await _db.SaveChangesAsync();

            var token = GenerateJwtTokenWithTenant(user.Id, user.Email, tenant.Id, "owner");
            return new AuthResponse(true, token, user.Id.ToString(), null, new List<TenantInfo> { new TenantInfo(tenant.Id, tenant.Name, "owner") }, false);
        }
        else
        {
            await _db.SaveChangesAsync();

            var memberships = await _db.UserTenantMemberships
                .Where(m => m.UserId == user.Id && m.Status == "active")
                .Include(m => m.Tenant)
                .Select(m => new TenantInfo(m.TenantId, m.Tenant.Name, m.Role))
                .ToListAsync();

            return new AuthResponse(true, null, user.Id.ToString(), null, memberships, false);
        }
    }

    public async Task<AuthResponse> LoginAsync(string email, string password)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return new AuthResponse(false, null, null, "Invalid email or password", null, false);

        user.LastLoginAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        if (user.IsAdmin)
        {
            var adminToken = GenerateJwtTokenWithTenant(user.Id, user.Email, Guid.Empty, "admin");
            return new AuthResponse(true, adminToken, user.Id.ToString(), null, null, true);
        }

        var memberships = await _db.UserTenantMemberships
            .Where(m => m.UserId == user.Id && m.Status == "active")
            .Include(m => m.Tenant)
            .Select(m => new TenantInfo(m.TenantId, m.Tenant.Name, m.Role))
            .ToListAsync();

        if (memberships.Count == 1)
        {
            var m = memberships[0];
            var token = GenerateJwtTokenWithTenant(user.Id, user.Email, m.TenantId, m.Role);
            return new AuthResponse(true, token, user.Id.ToString(), null, memberships, false);
        }

        // Multi-tenant or no tenants: return a token so select-tenant can authenticate
        var preToken = GenerateJwtToken(user.Id, user.Email);
        return new AuthResponse(true, preToken, user.Id.ToString(), null, memberships, false);
    }

    public string GenerateJwtTokenWithTenant(Guid userId, string email, Guid tenantId, string role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim("tenantId", tenantId.ToString()),
            new Claim("role", role)
        };
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"], audience: _config["Jwt:Audience"],
            claims: claims, expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateJwtToken(Guid userId, string email)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Email, email)
        };
        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"], audience: _config["Jwt:Audience"],
            claims: claims, expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public record AuthResponse(bool Success, string? Token, string? UserId, string? Error, List<TenantInfo>? Tenants, bool IsAdmin);
public record TenantInfo(Guid TenantId, string Name, string Role);
