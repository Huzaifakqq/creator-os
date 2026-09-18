using System;
using System.Collections.Generic;

namespace CreatorOS.Domain;

public partial class AppVersion
{
    public Guid Id { get; set; }

    public Guid AppId { get; set; }

    public int VersionNumber { get; set; }

    public string Config { get; set; } = null!;

    public string? Changelog { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual App App { get; set; } = null!;

    public virtual ICollection<AppDeployment> AppDeployments { get; set; } = new List<AppDeployment>();
}
