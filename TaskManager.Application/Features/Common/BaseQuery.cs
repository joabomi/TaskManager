using TaskManager.Application.Contracts.Persistence;

namespace TaskManager.Application.Features.Common;

public record BaseQuery : IPaginationParameters, ISortParameters
{
    private const int MaxPageSize = 20;

    public int PageNumber { get; set; } = 1;

    private int _pageSize = 50;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value < 0 || value > MaxPageSize) ? MaxPageSize : value;
    }

    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = false;
}