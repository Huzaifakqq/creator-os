using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class Integration
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Provider { get; set; } = null!;

    public string? DisplayName { get; set; }

    public string AuthType { get; set; } = null!;

    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }

    public string? ApiKey { get; set; }

    public string? Config { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? LastSyncAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
