using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AppDatabase
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid AppId { get; set; }

    public string TableName { get; set; } = null!;

    public string TableSchema { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual App App { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
