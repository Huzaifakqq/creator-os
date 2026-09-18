using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class SecurityAuditLog
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid? UserId { get; set; }

    public string Action { get; set; } = null!;

    public string? EntityType { get; set; }

    public Guid? EntityId { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public string? Metadata { get; set; }

    public string RiskLevel { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
