using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class Refund
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid OrderId { get; set; }

    public Guid? PaymentId { get; set; }

    public string? StripeRefundId { get; set; }

    public long AmountCents { get; set; }

    public string? Reason { get; set; }

    public string Status { get; set; } = null!;

    public Guid? ProcessedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Payment? Payment { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
