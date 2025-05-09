using MediatR;
using TaskManager.Application.Features.Common;

namespace TaskManager.Application.Features.WorkTask.Queries.GetAllWorkTasks;

public record GetAllWorkTasksQuery() : BaseQuery, IRequest<List<WorkTaskDto>>
{
    public bool IsLoggedUser { get; set; } = false;
    public bool IsLoggedAdmin { get; set; } = false;

    public string? Name_Filter { get; set; }
    public string? Description_Filter { get; set; }
    public int? StatusId_Filter { get; set; }
    public int? PriorityId_Filter { get; set; }
    public DateTime? From_StartDate { get; set; }
    public DateTime? To_StartDate { get; set; }
    public DateTime? From_EndDate { get; set; }
    public DateTime? To_EndDate { get; set; }
    public string? AssignedPersonId_Filter { get; set; }
}
