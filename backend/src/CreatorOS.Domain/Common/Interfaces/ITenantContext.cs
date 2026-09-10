namespace CreatorOS.Domain.Common.Interfaces;

public interface ITenantContext
{
    Guid? TenantId { get; }
    void SetTenantId(Guid tenantId);
}

public interface ICurrentUserService
{
    Guid? UserId { get; }
    string? Email { get; }
    bool IsAuthenticated { get; }
}
