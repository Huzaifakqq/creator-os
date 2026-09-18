using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class EmailSubscriber
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid ListId { get; set; }

    public Guid? ContactId { get; set; }

    public string Email { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime SubscribedAt { get; set; }

    public DateTime? UnsubscribedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual EmailList List { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
