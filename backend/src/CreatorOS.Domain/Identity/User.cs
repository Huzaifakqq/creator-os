using CreatorOS.Domain.Common.Base;

namespace CreatorOS.Domain.Identity;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
    public DateTime? LastLoginAt { get; set; }

    public ICollection<UserTenantMembership> Memberships { get; set; } = new List<UserTenantMembership>();
}

public class UserTenantMembership : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Role { get; set; } = string.Empty; // owner, admin, editor, support, finance, analyst
    public string Status { get; set; } = "active";

    public User User { get; set; } = null!;
}

public class RefreshToken : BaseEntity
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }

    public User User { get; set; } = null!;
}
