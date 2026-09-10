using CreatorOS.Domain.Common.Base;

namespace CreatorOS.Domain.Workspace;

public class App : TenantEntity
{
    public Guid WorkspaceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Type { get; set; } = string.Empty; // website, webapp, storefront, course, membership
    public string Status { get; set; } = "draft";
    public string? Config { get; set; } // JSON
    public string? CustomDomain { get; set; }
    public int PublishedVersion { get; set; }

    public Workspace Workspace { get; set; } = null!;
    public ICollection<AppVersion> Versions { get; set; } = new List<AppVersion>();
}

public class AppVersion : BaseEntity
{
    public Guid AppId { get; set; }
    public int VersionNumber { get; set; }
    public string Config { get; set; } = string.Empty;
    public string? Changelog { get; set; }
    public Guid? CreatedBy { get; set; }

    public App App { get; set; } = null!;
}
