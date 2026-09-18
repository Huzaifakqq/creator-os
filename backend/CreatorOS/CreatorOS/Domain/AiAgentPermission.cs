using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AiAgentPermission
{
    public Guid Id { get; set; }

    public Guid AgentId { get; set; }

    public string PermissionType { get; set; } = null!;

    public bool RequiresApproval { get; set; }

    public long AutoApproveBelowCents { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual AiAgent Agent { get; set; } = null!;
}
