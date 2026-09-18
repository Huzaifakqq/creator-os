using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class MarketplaceListingMetric
{
    public Guid Id { get; set; }

    public Guid ListingId { get; set; }

    public DateOnly Date { get; set; }

    public int ViewCount { get; set; }

    public int ClickCount { get; set; }

    public int AddToCartCount { get; set; }

    public int PurchaseCount { get; set; }

    public long RevenueCents { get; set; }

    public string Source { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual MarketplaceListing Listing { get; set; } = null!;
}
