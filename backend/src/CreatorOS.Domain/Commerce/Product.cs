using CreatorOS.Domain.Common.Base;

namespace CreatorOS.Domain.Commerce;

public class Product : TenantEntity
{
    public Guid AppId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string ProductType { get; set; } = string.Empty;
    public long PriceCents { get; set; }
    public string Currency { get; set; } = "USD";
    public string BillingType { get; set; } = "one_time";
    public string? RecurringInterval { get; set; }
    public int TrialDays { get; set; }
    public bool IsActive { get; set; } = true;
    public string? ThumbnailUrl { get; set; }
    public string? DigitalAssets { get; set; } // JSON
}

public class Order : TenantEntity
{
    public Guid? CustomerId { get; set; }
    public string? StripeSessionId { get; set; }
    public string? StripePaymentIntentId { get; set; }
    public string Status { get; set; } = "pending";
    public long SubtotalCents { get; set; }
    public long DiscountCents { get; set; }
    public long TotalCents { get; set; }
    public string Currency { get; set; } = "USD";
    public string Source { get; set; } = "storefront";
    public string? Metadata { get; set; }
    public DateTime? CompletedAt { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public long UnitPriceCents { get; set; }
    public long TotalCents { get; set; }

    public Order Order { get; set; } = null!;
}
