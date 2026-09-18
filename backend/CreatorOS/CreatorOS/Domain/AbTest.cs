using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AbTest
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string TestType { get; set; } = null!;

    public string TargetEntity { get; set; } = null!;

    public Guid TargetEntityId { get; set; }

    public string Variants { get; set; } = null!;

    public string Status { get; set; } = null!;

    public Guid? WinnerVariantId { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? EndedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
