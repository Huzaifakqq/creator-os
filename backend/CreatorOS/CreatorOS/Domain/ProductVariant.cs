using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class ProductVariant
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid ProductId { get; set; }

    public string VariantName { get; set; } = null!;

    public long PriceCents { get; set; }

    public string Currency { get; set; } = null!;

    public string BillingType { get; set; } = null!;

    public string? RecurringInterval { get; set; }

    public int TrialDays { get; set; }

    public string? Features { get; set; }

    public string? Quotas { get; set; }

    public int SortOrder { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
