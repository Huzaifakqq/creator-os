using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AiAgentTask
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid AgentId { get; set; }

    public string TaskType { get; set; } = null!;

    public string InputData { get; set; } = null!;

    public string? OutputData { get; set; }

    public string Status { get; set; } = null!;

    public bool RequiresApproval { get; set; }

    public Guid? ApprovedBy { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public string? RejectionReason { get; set; }

    public int TokensUsed { get; set; }

    public int CostCents { get; set; }

    public string? ErrorMessage { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual AiAgent Agent { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
