using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class TemplateRemix
{
    public Guid Id { get; set; }

    public Guid OriginalTemplateId { get; set; }

    public Guid RemixAppId { get; set; }

    public Guid TenantId { get; set; }

    public string LicenseType { get; set; } = null!;

    public decimal RoyaltyPercent { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Template OriginalTemplate { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
