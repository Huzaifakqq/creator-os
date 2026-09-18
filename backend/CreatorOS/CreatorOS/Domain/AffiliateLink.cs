using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AffiliateLink
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid MemberId { get; set; }

    public string Code { get; set; } = null!;

    public string TargetUrl { get; set; } = null!;

    public int ClickCount { get; set; }

    public int ConversionCount { get; set; }

    public long RevenueCents { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<AffiliateConversion> AffiliateConversions { get; set; } = new List<AffiliateConversion>();

    public virtual AffiliateMember Member { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
