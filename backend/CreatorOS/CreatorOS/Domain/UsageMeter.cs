using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class UsageMeter
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string MeterType { get; set; } = null!;

    public DateTime PeriodStart { get; set; }

    public DateTime PeriodEnd { get; set; }

    public long UsedAmount { get; set; }

    public long QuotaAmount { get; set; }

    public int AlertThresholdPercent { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
