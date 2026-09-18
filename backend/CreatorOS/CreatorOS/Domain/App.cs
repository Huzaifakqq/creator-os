using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class App
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid WorkspaceId { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Description { get; set; }

    public string Type { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? Config { get; set; }

    public string? CustomDomain { get; set; }

    public int PublishedVersion { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<AppComponent> AppComponents { get; set; } = new List<AppComponent>();

    public virtual ICollection<AppDatabase> AppDatabases { get; set; } = new List<AppDatabase>();

    public virtual ICollection<AppDeployment> AppDeployments { get; set; } = new List<AppDeployment>();

    public virtual ICollection<AppFeature> AppFeatures { get; set; } = new List<AppFeature>();

    public virtual ICollection<AppLog> AppLogs { get; set; } = new List<AppLog>();

    public virtual ICollection<AppVersion> AppVersions { get; set; } = new List<AppVersion>();

    public virtual ICollection<Page> Pages { get; set; } = new List<Page>();

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;
}
