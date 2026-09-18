using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class EmailSequenceEnrollment
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid SequenceId { get; set; }

    public Guid ContactId { get; set; }

    public int CurrentStep { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? NextSendAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual CrmContact Contact { get; set; } = null!;

    public virtual EmailSequence Sequence { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
