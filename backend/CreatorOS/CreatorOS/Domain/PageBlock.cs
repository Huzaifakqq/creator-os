using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class PageBlock
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid PageId { get; set; }

    public string BlockType { get; set; } = null!;

    public string Config { get; set; } = null!;

    public int SortOrder { get; set; }

    public int ColumnSpan { get; set; }

    public bool IsVisible { get; set; }

    public string? ResponsiveConfig { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Page Page { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
