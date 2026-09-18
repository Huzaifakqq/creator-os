using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class Deal
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid PipelineId { get; set; }

    public Guid? ContactId { get; set; }

    public string Title { get; set; } = null!;

    public long ValueCents { get; set; }

    public string Currency { get; set; } = null!;

    public string Stage { get; set; } = null!;

    public int Probability { get; set; }

    public DateTime? ExpectedCloseDate { get; set; }

    public string Status { get; set; } = null!;

    public string? Notes { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual CrmContact? Contact { get; set; }

    public virtual DealPipeline Pipeline { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
