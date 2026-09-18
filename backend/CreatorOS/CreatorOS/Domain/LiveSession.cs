using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class LiveSession
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid HostId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string SessionType { get; set; } = null!;

    public string? StreamUrl { get; set; }

    public string? StreamKey { get; set; }

    public int MaxViewers { get; set; }

    public int CurrentViewers { get; set; }

    public bool IsLive { get; set; }

    public DateTime? ScheduledAt { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? EndedAt { get; set; }

    public string? RecordingUrl { get; set; }

    public bool ChatEnabled { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User Host { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
