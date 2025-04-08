using TaskManager.Domain;

namespace TaskManager.Application.Contracts.Persistence;

public interface IWorkTaskRepository : IGenericRepository<WorkTask>
{
    Task<WorkTask> GetWorkTaskWithDetails(int id);
    Task<List<WorkTask>> GetWorkTasksWithDetails();
    Task<List<WorkTask>> GetWorkTasksWithDetails(string userId);
}