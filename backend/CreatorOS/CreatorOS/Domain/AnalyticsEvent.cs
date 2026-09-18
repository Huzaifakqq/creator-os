using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AnalyticsEvent
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string EventType { get; set; } = null!;

    public string? EntityType { get; set; }

    public Guid? EntityId { get; set; }

    public Guid? UserId { get; set; }

    public string? SessionId { get; set; }

    public string? Metadata { get; set; }

    public string? Source { get; set; }

    public string? DeviceType { get; set; }

    public string? CountryCode { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
