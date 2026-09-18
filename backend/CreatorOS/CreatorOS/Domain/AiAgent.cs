using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AiAgent
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string AgentType { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string SystemPrompt { get; set; } = null!;

    public string? Capabilities { get; set; }

    public string ModelProvider { get; set; } = null!;

    public string ModelName { get; set; } = null!;

    public int MonthlyCreditLimit { get; set; }

    public int CreditsUsed { get; set; }

    public string Status { get; set; } = null!;

    public bool RequiresApproval { get; set; }

    public string? ApprovalTypes { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<AiAgentAuditLog> AiAgentAuditLogs { get; set; } = new List<AiAgentAuditLog>();

    public virtual ICollection<AiAgentPermission> AiAgentPermissions { get; set; } = new List<AiAgentPermission>();

    public virtual ICollection<AiAgentTask> AiAgentTasks { get; set; } = new List<AiAgentTask>();

    public virtual Tenant Tenant { get; set; } = null!;
}
