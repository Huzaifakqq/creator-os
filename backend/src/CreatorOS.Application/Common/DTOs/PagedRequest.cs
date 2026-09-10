using MediatR;

namespace CreatorOS.Application.Common.DTOs;

public record PagedRequest : IRequest<PagedResult<object>>
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
    public string? Search { get; init; }
    public string? SortBy { get; init; }
    public bool SortDescending { get; init; }
}
