using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AppLog
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid AppId { get; set; }

    public string LogLevel { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? Source { get; set; }

    public string? StackTrace { get; set; }

    public string? Metadata { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual App App { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
