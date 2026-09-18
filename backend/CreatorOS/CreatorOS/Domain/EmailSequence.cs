using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class EmailSequence
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string TriggerType { get; set; } = null!;

    public string? TriggerConfig { get; set; }

    public string Steps { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int EnrolledCount { get; set; }

    public int CompletedCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<EmailSequenceEnrollment> EmailSequenceEnrollments { get; set; } = new List<EmailSequenceEnrollment>();

    public virtual Tenant Tenant { get; set; } = null!;
}
