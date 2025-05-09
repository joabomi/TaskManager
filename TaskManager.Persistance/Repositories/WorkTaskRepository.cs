using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTask.Queries.GetAllWorkTasks;
using TaskManager.Domain;
using TaskManager.Domain.Common;
using TaskManager.Persistance.DatabaseContext;

namespace TaskManager.Persistance.Repositories;

public class WorkTaskRepository : GenericRepository<WorkTask>, IWorkTaskRepository
{
    public WorkTaskRepository(TaskManagerDatabaseContext context) : base(context)
    {
    }

    public async Task<WorkTask> GetWorkTaskWithDetails(int id)
    {
        var worktask = await _context.WorkTasks.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);
        return worktask;
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
            .Where(q => q.AssignedPersonId == userId)
            .Include(q => q.Priority)
            .Include(q => q.Status)
            .ToListAsync();
        return worktasks;
    }

    public Task<PagedResult<WorkTask>> GetWorkTasksWithDetails(GetAllWorkTasksQuery query)
    {
        var baseQuery = _context.WorkTasks
            .Include(q => q.Priority)
            .Include(q => q.Status)
            .AsQueryable();

        baseQuery = baseQuery.Where(q =>
            (string.IsNullOrEmpty(query.Name_Filter) || q.Name.Contains(query.Name_Filter)) &&
            (string.IsNullOrEmpty(query.Description_Filter) || q.Description.Contains(query.Description_Filter)) &&
            (query.PriorityId_Filter == null || q.PriorityId == query.PriorityId_Filter) &&
            (query.StatusId_Filter == null || q.StatusId == query.StatusId_Filter) &&
            (query.From_StartDate == null || q.StartDate >= query.From_StartDate) &&
            (query.To_StartDate == null || q.StartDate <= query.To_StartDate) &&
            (query.From_EndDate == null || q.EndDate >= query.From_EndDate) &&
            (query.To_EndDate == null || q.EndDate <= query.To_EndDate) &&
            (string.IsNullOrEmpty(query.AssignedPersonId_Filter) || q.AssignedPersonId == query.AssignedPersonId_Filter)
        );

        return GetPagedAsync(baseQuery, query);
    }

    public Task<PagedResult<WorkTask>> GetWorkTasksWithDetails(string userId, GetAllWorkTasksQuery query)
    {
        var baseQuery = _context.WorkTasks
            .Where(q => q.AssignedPersonId == userId)
            .Include(q => q.Priority)
            .Include(q => q.Status)
            .AsQueryable();

        baseQuery = baseQuery.Where(q =>
            (string.IsNullOrEmpty(query.Name_Filter) || q.Name.Contains(query.Name_Filter)) &&
            (string.IsNullOrEmpty(query.Description_Filter) || q.Description.Contains(query.Description_Filter)) &&
            (query.PriorityId_Filter == null || q.PriorityId == query.PriorityId_Filter) &&
            (query.StatusId_Filter == null || q.StatusId == query.StatusId_Filter) &&
            (query.From_StartDate == null || q.StartDate >= query.From_StartDate) &&
            (query.To_StartDate == null || q.StartDate <= query.To_StartDate) &&
            (query.From_EndDate == null || q.EndDate >= query.From_EndDate) &&
            (query.To_EndDate == null || q.EndDate <= query.To_EndDate)
        );

        return GetPagedAsync(baseQuery, query);
    }
}