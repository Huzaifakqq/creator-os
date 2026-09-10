namespace CreatorOS.Domain.Common.Base;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public abstract class TenantEntity : BaseEntity
{
    public Guid TenantId { get; set; }
}
