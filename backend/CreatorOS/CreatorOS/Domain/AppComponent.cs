using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AppComponent
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid AppId { get; set; }

    public string Name { get; set; } = null!;

    public string ComponentType { get; set; } = null!;

    public string Config { get; set; } = null!;

    public int SortOrder { get; set; }

    public bool IsReusable { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual App App { get; set; } = null!;

    public virtual Tenant Tenant { get; set; } = null!;
}
