using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class PricingCalculation
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string? Name { get; set; }

    public string Config { get; set; } = null!;

    public string? ResultData { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
