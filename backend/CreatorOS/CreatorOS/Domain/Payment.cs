using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid OrderId { get; set; }

    public string? StripePaymentIntentId { get; set; }

    public string? StripeChargeId { get; set; }

    public long AmountCents { get; set; }

    public string Currency { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? PaymentMethod { get; set; }

    public string? FailureReason { get; set; }

    public DateTime? RefundedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();

    public virtual Tenant Tenant { get; set; } = null!;
}
