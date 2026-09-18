using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class DomainSetting
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid? AppId { get; set; }

    public string Domain { get; set; } = null!;

    public bool IsCustom { get; set; }

    public string SslStatus { get; set; } = null!;

    public DateTime? SslExpiresAt { get; set; }

    public string? DnsVerificationToken { get; set; }

    public bool IsVerified { get; set; }

    public DateTime? VerifiedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
