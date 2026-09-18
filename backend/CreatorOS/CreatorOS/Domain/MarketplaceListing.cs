using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class MarketplaceListing
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid ProductId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? ShortDescription { get; set; }

    public string Category { get; set; } = null!;

    public string? Tags { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string? GalleryUrls { get; set; }

    public long PriceCents { get; set; }

    public long? OriginalPriceCents { get; set; }

    public string LicenseType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? RejectionReason { get; set; }

    public Guid? ReviewedBy { get; set; }

    public DateTime? ReviewedAt { get; set; }

    public DateTime? FeaturedAt { get; set; }

    public int TotalSales { get; set; }

    public long TotalRevenueCents { get; set; }

    public decimal? Rating { get; set; }

    public int RatingCount { get; set; }

    public string Version { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<MarketplaceListingMetric> MarketplaceListingMetrics { get; set; } = new List<MarketplaceListingMetric>();

    public virtual ICollection<MarketplaceReview> MarketplaceReviews { get; set; } = new List<MarketplaceReview>();

    public virtual Product Product { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
