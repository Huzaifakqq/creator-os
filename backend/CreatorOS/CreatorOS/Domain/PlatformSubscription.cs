using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class PlatformSubscription
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string PlanName { get; set; } = null!;

    public string? StripeSubscriptionId { get; set; }

    public string? StripeCustomerId { get; set; }

    public string Status { get; set; } = null!;

    public long MonthlyPriceCents { get; set; }

    public decimal TransactionFeePercent { get; set; }

    public decimal MarketplaceFeePercent { get; set; }

    public int AiCreditsMonthly { get; set; }

    public int StorageGbMonthly { get; set; }

    public int EmailSendsMonthly { get; set; }

    public DateTime? CurrentPeriodStart { get; set; }

    public DateTime? CurrentPeriodEnd { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
