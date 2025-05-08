namespace TaskManager.Application.Features.Common;

public record BaseQuery
{
    public string? Sortby {get; set;}
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = -1;
    public bool SortDescending { get; set; } = false;
}