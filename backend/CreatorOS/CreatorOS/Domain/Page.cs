using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class Page
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid AppId { get; set; }

    public string Title { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string PageType { get; set; } = null!;

    public bool IsPublished { get; set; }

    public int PublishedVersion { get; set; }

    public string? SeoTitle { get; set; }

    public string? SeoDescription { get; set; }

    public string? SeoImage { get; set; }

    public string? CustomCss { get; set; }

    public string? CustomJs { get; set; }

    public int SortOrder { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual App App { get; set; } = null!;

    public virtual ICollection<PageBlock> PageBlocks { get; set; } = new List<PageBlock>();

    public virtual Tenant Tenant { get; set; } = null!;
}
