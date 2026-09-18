using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class UserConsent
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid TenantId { get; set; }

    public string ConsentType { get; set; } = null!;

    public bool IsGranted { get; set; }

    public string? ConsentText { get; set; }

    public string? IpAddress { get; set; }

    public DateTime GrantedAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
