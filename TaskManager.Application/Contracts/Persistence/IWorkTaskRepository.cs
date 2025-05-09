using TaskManager.Application.Features.WorkTask.Queries.GetAllWorkTasks;
using TaskManager.Domain;
using TaskManager.Domain.Common;

namespace TaskManager.Application.Contracts.Persistence;

public interface IWorkTaskRepository : IGenericRepository<WorkTask>
{
    Task<WorkTask> GetWorkTaskWithDetails(int id);
    Task<List<WorkTask>> GetWorkTasksWithDetails();
    Task<List<WorkTask>> GetWorkTasksWithDetails(string userId);
    Task<PagedResult<WorkTask>> GetWorkTasksWithDetails(GetAllWorkTasksQuery query);
    Task<PagedResult<WorkTask>> GetWorkTasksWithDetails(string userId, GetAllWorkTasksQuery query);
}