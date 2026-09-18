using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class MarketplaceReview
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid ListingId { get; set; }

    public Guid CustomerId { get; set; }

    public int Rating { get; set; }

    public string? Title { get; set; }

    public string? Comment { get; set; }

    public bool IsVerifiedPurchase { get; set; }

    public bool IsVisible { get; set; }

    public int HelpfulCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual MarketplaceListing Listing { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
