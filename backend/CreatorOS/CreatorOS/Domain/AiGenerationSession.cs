using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AiGenerationSession
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid UserId { get; set; }

    public Guid? AppId { get; set; }

    public string Prompt { get; set; } = null!;

    public string? Response { get; set; }

    public string? ModelUsed { get; set; }

    public int TokensUsed { get; set; }

    public int CostCents { get; set; }

    public string Status { get; set; } = null!;

    public string? ErrorMessage { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
