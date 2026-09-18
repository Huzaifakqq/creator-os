using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class CrmTag
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string? Color { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Tenant Tenant { get; set; } = null!;

    public virtual ICollection<CrmContact> Contacts { get; set; } = new List<CrmContact>();
}
