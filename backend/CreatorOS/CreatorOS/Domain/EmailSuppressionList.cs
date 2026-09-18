using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class EmailSuppressionList
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Email { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public DateTime SuppressedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
