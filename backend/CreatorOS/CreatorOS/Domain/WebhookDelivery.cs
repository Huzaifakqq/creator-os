using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class WebhookDelivery
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid WebhookId { get; set; }

    public string Event { get; set; } = null!;

    public string Payload { get; set; } = null!;

    public int? ResponseStatusCode { get; set; }

    public string? ResponseBody { get; set; }

    public string Status { get; set; } = null!;

    public int AttemptCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual Webhook Webhook { get; set; } = null!;
}
