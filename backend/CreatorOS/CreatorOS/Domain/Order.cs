using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class Order
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid? CustomerId { get; set; }

    public string? StripeSessionId { get; set; }

    public string? StripePaymentIntentId { get; set; }

    public string Status { get; set; } = null!;

    public long SubtotalCents { get; set; }

    public long DiscountCents { get; set; }

    public long TotalCents { get; set; }

    public string Currency { get; set; } = null!;

    public string Source { get; set; } = null!;

    public string? Metadata { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<AffiliateConversion> AffiliateConversions { get; set; } = new List<AffiliateConversion>();

    public virtual ICollection<Attribution> Attributions { get; set; } = new List<Attribution>();

    public virtual ICollection<Dispute> Disputes { get; set; } = new List<Dispute>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<OrderStatusHistory> OrderStatusHistories { get; set; } = new List<OrderStatusHistory>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();

    public virtual Tenant Tenant { get; set; } = null!;
}
