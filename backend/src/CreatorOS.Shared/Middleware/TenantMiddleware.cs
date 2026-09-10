using CreatorOS.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace CreatorOS.Shared.Tenant;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var tenantContext = context.RequestServices.GetRequiredService<ITenantContext>();

        // Extract tenant from JWT claim or header
        var tenantClaim = context.User?.FindFirst("activeTenant")?.Value;
        var tenantHeader = context.Request.Headers["X-Tenant-Id"].FirstOrDefault();

        var tenantId = tenantClaim ?? tenantHeader;

        if (Guid.TryParse(tenantId, out var parsedTenantId))
        {
            tenantContext.SetTenantId(parsedTenantId);
        }

        await _next(context);
    }
}
