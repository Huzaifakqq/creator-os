using CreatorOS.Domain.Common.Interfaces;

namespace CreatorOS.Shared.Tenant;

public class TenantContext : ITenantContext
{
    private Guid? _tenantId;

    public Guid? TenantId => _tenantId;

    public void SetTenantId(Guid tenantId)
    {
        _tenantId = tenantId;
    }
}
