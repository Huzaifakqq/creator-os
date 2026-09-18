using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AffiliateProgram
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string CommissionType { get; set; } = null!;

    public decimal CommissionValue { get; set; }

    public int CookieDays { get; set; }

    public long MinimumPayoutCents { get; set; }

    public string? AllowedPromotionMethods { get; set; }

    public string? TermsUrl { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<AffiliateMember> AffiliateMembers { get; set; } = new List<AffiliateMember>();

    public virtual Tenant Tenant { get; set; } = null!;
}
