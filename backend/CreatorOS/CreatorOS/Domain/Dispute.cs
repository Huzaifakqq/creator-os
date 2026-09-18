using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class Dispute
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid OrderId { get; set; }

    public Guid CustomerId { get; set; }

    public string Reason { get; set; } = null!;

    public string? Description { get; set; }

    public string Status { get; set; } = null!;

    public string? Resolution { get; set; }

    public long RefundAmountCents { get; set; }

    public Guid? ResolvedBy { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
