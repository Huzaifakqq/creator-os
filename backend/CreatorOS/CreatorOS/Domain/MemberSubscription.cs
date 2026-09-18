using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class MemberSubscription
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid TierId { get; set; }

    public Guid MemberId { get; set; }

    public Guid? SubscriptionId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CurrentPeriodEnd { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User Member { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual MembershipTier Tier { get; set; } = null!;
}
