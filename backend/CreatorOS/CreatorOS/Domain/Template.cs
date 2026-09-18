using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class Template
{
    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Category { get; set; } = null!;

    public string? ThumbnailUrl { get; set; }

    public string Config { get; set; } = null!;

    public bool IsPublic { get; set; }

    public bool IsPremium { get; set; }

    public int PriceCents { get; set; }

    public int UsageCount { get; set; }

    public decimal? Rating { get; set; }

    public int RatingCount { get; set; }

    public Guid? AuthorId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<TemplateRemix> TemplateRemixes { get; set; } = new List<TemplateRemix>();

    public virtual Tenant? Tenant { get; set; }
}
