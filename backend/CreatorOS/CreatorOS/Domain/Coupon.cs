using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class Coupon
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public string DiscountType { get; set; } = null!;

    public long DiscountValue { get; set; }

    public long MinimumOrderCents { get; set; }

    public long? MaximumDiscountCents { get; set; }

    public int? UsageLimit { get; set; }

    public int UsedCount { get; set; }

    public int PerUserLimit { get; set; }

    public string AppliesTo { get; set; } = null!;

    public string? AppliesToProductIds { get; set; }

    public DateTime StartsAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
