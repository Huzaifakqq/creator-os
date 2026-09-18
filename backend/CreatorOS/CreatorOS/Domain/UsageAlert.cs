using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class UsageAlert
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string MeterType { get; set; } = null!;

    public int ThresholdPercent { get; set; }

    public string? AlertEmail { get; set; }

    public string? AlertWebhookUrl { get; set; }

    public DateTime? LastTriggeredAt { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
