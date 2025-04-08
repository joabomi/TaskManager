using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Domain;
using TaskManager.Persistance.DatabaseContext;

namespace TaskManager.Persistance.Repositories;

public class WorkTaskRepository : GenericRepository<WorkTask>, IWorkTaskRepository
{
    public WorkTaskRepository(TaskManagerDatabaseContext context) : base(context)
    {
    }

    public async Task<List<WorkTask>> GetWorkTasksWithDetails()
    {
        var worktasks = await _context.WorkTasks
            .Include(q => q.Priority)
            .Include(q => q.Status)
            .ToListAsync();
        return worktasks;
    }

    public async Task<List<WorkTask>> GetWorkTasksWithDetails(string userId)
    {
        var worktasks = await _context.WorkTasks
            .Where(q => q.AssignedEmployeeId == userId)
            .Include(q => q.Priority)
            .Include(q => q.Status)
            .ToListAsync();
        return worktasks;
    }

    public async Task<WorkTask> GetWorkTaskWithDetails(int id)
    {
        var worktask = await _context.WorkTasks.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);
        return worktask;
    }
}