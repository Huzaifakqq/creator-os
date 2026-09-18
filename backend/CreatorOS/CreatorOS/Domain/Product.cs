using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class Product
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid? AppId { get; set; }

    public string Name { get; set; } = null!;

    public string? Slug { get; set; }

    public string? Description { get; set; }

    public string ProductType { get; set; } = null!;

    public long PriceCents { get; set; }

    public string Currency { get; set; } = null!;

    public string BillingType { get; set; } = null!;

    public string? RecurringInterval { get; set; }

    public int TrialDays { get; set; }

    public bool IsActive { get; set; }

    public bool IsPublished { get; set; }

    public DateTime? PublishedAt { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string? SeoTitle { get; set; }

    public string? SeoDescription { get; set; }

    public string? DigitalAssets { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<DigitalAsset> DigitalAssetsNavigation { get; set; } = new List<DigitalAsset>();

    public virtual ICollection<MarketplaceListing> MarketplaceListings { get; set; } = new List<MarketplaceListing>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

    public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    public virtual Tenant Tenant { get; set; } = null!;
}
