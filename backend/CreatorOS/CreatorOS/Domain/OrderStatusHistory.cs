using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class OrderStatusHistory
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid OrderId { get; set; }

    public string? FromStatus { get; set; }

    public string ToStatus { get; set; } = null!;

    public string? Notes { get; set; }

    public Guid? ChangedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
