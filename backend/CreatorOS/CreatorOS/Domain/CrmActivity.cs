using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class CrmActivity
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid ContactId { get; set; }

    public string ActivityType { get; set; } = null!;

    public string? Subject { get; set; }

    public string? Description { get; set; }

    public string? Direction { get; set; }

    public int? DurationSeconds { get; set; }

    public string? Metadata { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual CrmContact Contact { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
