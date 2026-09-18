using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class DealPipeline
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string Stages { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Deal> Deals { get; set; } = new List<Deal>();

    public virtual Tenant Tenant { get; set; } = null!;
}
