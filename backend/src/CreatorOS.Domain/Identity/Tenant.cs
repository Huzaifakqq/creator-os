using CreatorOS.Domain.Common.Base;

namespace CreatorOS.Domain.Identity;

public class Tenant : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Plan { get; set; } = "free";
    public string Status { get; set; } = "active";
    public string? Settings { get; set; } // JSON
}
