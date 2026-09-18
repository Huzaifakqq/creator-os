using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class OrderItem
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public Guid? ProductVariantId { get; set; }

    public int Quantity { get; set; }

    public long UnitPriceCents { get; set; }

    public long TotalCents { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
