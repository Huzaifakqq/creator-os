using CreatorOS.Domain.Identity;
using MediatR;

namespace CreatorOS.Application.Identity.Commands;

public record RegisterCommand : IRequest<AuthResult>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string DisplayName { get; init; } = string.Empty;
}

public record LoginCommand : IRequest<AuthResult>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}

public record AuthResult
{
    public bool Success { get; init; }
    public string? AccessToken { get; init; }
    public string? RefreshToken { get; init; }
    public string? UserId { get; init; }
    public string? Error { get; init; }
}
