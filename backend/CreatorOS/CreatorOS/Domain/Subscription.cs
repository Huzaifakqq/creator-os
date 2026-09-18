using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class Subscription
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid CustomerId { get; set; }

    public Guid ProductId { get; set; }

    public Guid? ProductVariantId { get; set; }

    public string? StripeSubscriptionId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CurrentPeriodStart { get; set; }

    public DateTime CurrentPeriodEnd { get; set; }

    public DateTime? CancelAt { get; set; }

    public DateTime? CanceledAt { get; set; }

    public DateTime? TrialStart { get; set; }

    public DateTime? TrialEnd { get; set; }

    public string? Metadata { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
