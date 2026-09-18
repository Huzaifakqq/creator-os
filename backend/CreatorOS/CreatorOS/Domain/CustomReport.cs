using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class CustomReport
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string ReportType { get; set; } = null!;

    public string QueryConfig { get; set; } = null!;

    public string? ScheduleConfig { get; set; }

    public DateTime? LastRunAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;
}
