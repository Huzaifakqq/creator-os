using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class ContentDraft
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid UserId { get; set; }

    public string DraftType { get; set; } = null!;

    public string? Title { get; set; }

    public string Content { get; set; } = null!;

    public string? Platform { get; set; }

    public string Status { get; set; } = null!;

    public string? AiModelUsed { get; set; }

    public int TokensUsed { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
