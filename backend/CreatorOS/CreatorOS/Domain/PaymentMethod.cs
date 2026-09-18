using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class PaymentMethod
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid CustomerId { get; set; }

    public string StripePaymentMethodId { get; set; } = null!;

    public string? Brand { get; set; }

    public string? Last4 { get; set; }

    public int? ExpiryMonth { get; set; }

    public int? ExpiryYear { get; set; }

    public bool IsDefault { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
