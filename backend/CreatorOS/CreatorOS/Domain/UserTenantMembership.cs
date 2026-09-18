using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class UserTenantMembership
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid TenantId { get; set; }

    public string Role { get; set; } = null!;

    public string? Permissions { get; set; }

    public string Status { get; set; } = null!;

    public Guid? InvitedBy { get; set; }

    public DateTime? InvitedAt { get; set; }

    public DateTime? JoinedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
