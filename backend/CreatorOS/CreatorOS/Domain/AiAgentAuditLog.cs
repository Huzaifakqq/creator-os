using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AiAgentAuditLog
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid AgentId { get; set; }

    public Guid? TaskId { get; set; }

    public string Action { get; set; } = null!;

    public string? EntityType { get; set; }

    public Guid? EntityId { get; set; }

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public string? Metadata { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual AiAgent Agent { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
