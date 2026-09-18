using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class Invoice
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string InvoiceNumber { get; set; } = null!;

    public string? CustomerEmail { get; set; }

    public string? CustomerName { get; set; }

    public string LineItems { get; set; } = null!;

    public long SubtotalCents { get; set; }

    public long TaxCents { get; set; }

    public long TotalCents { get; set; }

    public string Currency { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string? StripeInvoiceId { get; set; }

    public DateTime? PaidAt { get; set; }

    public DateTime? DueDate { get; set; }

    public string? PdfUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
