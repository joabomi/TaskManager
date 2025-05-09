namespace TaskManager.Application.Contracts.Persistence;

public interface ISortParameters
{

    public string? SortBy { get; set; }

    public bool SortDescending { get; set; }
}
