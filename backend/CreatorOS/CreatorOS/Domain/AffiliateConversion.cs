using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AffiliateConversion
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid LinkId { get; set; }

    public Guid OrderId { get; set; }

    public Guid CustomerId { get; set; }

    public long CommissionCents { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? ApprovedAt { get; set; }

    public DateTime? PaidAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual AffiliateLink Link { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
