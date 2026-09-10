using CreatorOS.Domain.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CreatorOS.Shared.Auth;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var claim = _httpContextAccessor.HttpContext?.User?.FindFirst("sub");
            return claim != null ? Guid.Parse(claim.Value) : null;
        }
    }

    public string? Email =>
        _httpContextAccessor.HttpContext?.User?.FindFirst("email")?.Value;

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}
