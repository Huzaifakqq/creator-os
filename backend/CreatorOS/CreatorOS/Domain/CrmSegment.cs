using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class CrmSegment
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string FilterRules { get; set; } = null!;

    public int ContactCount { get; set; }

    public bool IsDynamic { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
