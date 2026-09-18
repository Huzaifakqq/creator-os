using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class EmailTemplate
{
    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string? ThumbnailUrl { get; set; }

    public string? HtmlContent { get; set; }

    public string? JsonContent { get; set; }

    public bool IsSystem { get; set; }

    public int UsageCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Tenant? Tenant { get; set; }
}
