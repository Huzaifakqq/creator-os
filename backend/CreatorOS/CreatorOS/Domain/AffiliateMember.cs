using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AffiliateMember
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid ProgramId { get; set; }

    public Guid AffiliateId { get; set; }

    public decimal? CustomCommissionRate { get; set; }

    public string Tier { get; set; } = null!;

    public long TotalEarningsCents { get; set; }

    public long PendingPayoutCents { get; set; }

    public int TotalReferrals { get; set; }

    public int TotalConversions { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User Affiliate { get; set; } = null!;

    public virtual ICollection<AffiliateLink> AffiliateLinks { get; set; } = new List<AffiliateLink>();

    public virtual ICollection<AffiliatePayout> AffiliatePayouts { get; set; } = new List<AffiliatePayout>();

    public virtual AffiliateProgram Program { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
