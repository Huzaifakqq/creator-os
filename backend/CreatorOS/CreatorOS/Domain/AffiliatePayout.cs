using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AffiliatePayout
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid MemberId { get; set; }

    public long AmountCents { get; set; }

    public string PayoutMethod { get; set; } = null!;

    public string? PayoutReference { get; set; }

    public string Status { get; set; } = null!;

    public DateTime PeriodStart { get; set; }

    public DateTime PeriodEnd { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual AffiliateMember Member { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
