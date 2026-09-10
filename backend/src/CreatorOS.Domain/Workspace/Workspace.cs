using CreatorOS.Domain.Common.Base;

namespace CreatorOS.Domain.Workspace;

public class Workspace : TenantEntity
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? CustomDomain { get; set; }
    public string? Settings { get; set; } // JSON

    public ICollection<App> Apps { get; set; } = new List<App>();
}
