using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AppDeployment
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid AppId { get; set; }

    public Guid VersionId { get; set; }

    public string? DeploymentUrl { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? DeployedAt { get; set; }

    public DateTime? RolledBackAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual App App { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual AppVersion Version { get; set; } = null!;
}
