using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class DirectMessage
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public string Content { get; set; } = null!;

    public string? MediaType { get; set; }

    public string? MediaUrl { get; set; }

    public bool IsRead { get; set; }

    public DateTime? ReadAt { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
