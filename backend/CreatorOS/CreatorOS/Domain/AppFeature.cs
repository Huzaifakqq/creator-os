using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AppFeature
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid AppId { get; set; }

    public string FeatureName { get; set; } = null!;

    public string FeatureType { get; set; } = null!;

    public string? Config { get; set; }

    public int SortOrder { get; set; }

    public bool IsEnabled { get; set; }

    public Guid? ParentFeatureId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual App App { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
