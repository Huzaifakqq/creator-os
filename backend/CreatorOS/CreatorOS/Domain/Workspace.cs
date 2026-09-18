using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class Workspace
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? CustomDomain { get; set; }

    public string? Settings { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<App> Apps { get; set; } = new List<App>();

    public virtual Tenant Tenant { get; set; } = null!;
}
