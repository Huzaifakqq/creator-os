using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class Webhook
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string? Secret { get; set; }

    public string Events { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? LastTriggeredAt { get; set; }

    public int? LastStatus { get; set; }

    public int FailureCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual ICollection<WebhookDelivery> WebhookDeliveries { get; set; } = new List<WebhookDelivery>();
}
