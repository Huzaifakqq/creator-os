using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class EmailList
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int SubscriberCount { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<EmailSubscriber> EmailSubscribers { get; set; } = new List<EmailSubscriber>();

    public virtual Tenant Tenant { get; set; } = null!;
}
