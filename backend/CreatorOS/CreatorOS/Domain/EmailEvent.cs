using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class EmailEvent
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid? CampaignId { get; set; }

    public Guid? SubscriberId { get; set; }

    public string EventType { get; set; } = null!;

    public string? Metadata { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
