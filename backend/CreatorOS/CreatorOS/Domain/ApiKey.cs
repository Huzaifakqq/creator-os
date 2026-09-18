using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class ApiKey
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string KeyHash { get; set; } = null!;

    public string KeyPrefix { get; set; } = null!;

    public string? Permissions { get; set; }

    public int RateLimit { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public bool IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
