using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class MembershipTier
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid? ProductVariantId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public long PriceCents { get; set; }

    public string Currency { get; set; } = null!;

    public string BillingInterval { get; set; } = null!;

    public string? Features { get; set; }

    public string? AccessRules { get; set; }

    public string? Color { get; set; }

    public string? IconUrl { get; set; }

    public int MemberCount { get; set; }

    public int SortOrder { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CommunitySpace> CommunitySpaces { get; set; } = new List<CommunitySpace>();

    public virtual ICollection<MemberSubscription> MemberSubscriptions { get; set; } = new List<MemberSubscription>();

    public virtual Tenant Tenant { get; set; } = null!;
}
