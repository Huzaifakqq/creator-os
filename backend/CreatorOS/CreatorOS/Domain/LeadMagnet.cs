using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class LeadMagnet
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string AssetType { get; set; } = null!;

    public string? AssetUrl { get; set; }

    public string? LandingPageConfig { get; set; }

    public string? ThankYouPageConfig { get; set; }

    public string? FormFields { get; set; }

    public int TotalDownloads { get; set; }

    public decimal? ConversionRate { get; set; }

    public bool IsPublished { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
