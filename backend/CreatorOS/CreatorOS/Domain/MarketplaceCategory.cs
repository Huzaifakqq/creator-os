using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class MarketplaceCategory
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Description { get; set; }

    public string? Icon { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public int SortOrder { get; set; }

    public bool IsActive { get; set; }

    public int ListingCount { get; set; }

    public DateTime CreatedAt { get; set; }
}
