namespace CreatorOS.Middleware;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var tenantClaim = context.User?.FindFirst("TenantId")?.Value;
        var tenantHeader = context.Request.Headers["X-Tenant-Id"].FirstOrDefault();

        var tenantId = tenantClaim ?? tenantHeader;

        if (Guid.TryParse(tenantId, out var parsedTenantId))
        {
            context.Items["TenantId"] = parsedTenantId;
        }

        await _next(context);
    }
}
