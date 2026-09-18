using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class Attribution
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid OrderId { get; set; }

    public string Source { get; set; } = null!;

    public Guid? SourceId { get; set; }

    public Guid? AffiliateId { get; set; }

    public Guid? ListingId { get; set; }

    public string? ReferralCode { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
