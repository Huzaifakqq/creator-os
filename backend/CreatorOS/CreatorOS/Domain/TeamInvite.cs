using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class TeamInvite
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Token { get; set; } = null!;

    public Guid InvitedBy { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime? AcceptedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
