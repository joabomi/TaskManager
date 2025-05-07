using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Models.Persistance;
using TaskManager.Domain;
using TaskManager.Domain.Common;
using TaskManager.Persistance.DatabaseContext;

namespace TaskManager.Persistance.Repositories;

public class WorkTaskPriorityTypeRepository : GenericRepository<WorkTaskPriorityType>, IWorkTaskPriorityTypeRepository
{
    public WorkTaskPriorityTypeRepository(TaskManagerDatabaseContext context) : base(context)
    {
    }

    public async Task<bool> IsWorkPriorityTypeUnique(string name, int weight)
    {
        return !await _context.WorkTaskPriorityTypes.AnyAsync(q => q.Name == name || q.PriorityWeight == weight);
    }

    public async Task<bool> IsWorkPriorityTypeUpdateValid(string name, int weight, int id)
    {
        return !await _context.WorkTaskPriorityTypes.AnyAsync(q => (q.Name == name || q.PriorityWeight == weight) && q.Id != id);
    }

    public async Task<PagedResult<WorkTaskPriorityType>> GetPagedAsync(WorkTaskPriorityTypeQueryParameters parameters)
    {
        var baseQuery = _context.WorkTaskPriorityTypes.AsQueryable();

        baseQuery = baseQuery.Where(q =>
            (string.IsNullOrEmpty(parameters.Name_Filter) || q.Name.Contains(parameters.Name_Filter)) &&
            (parameters.MinWeight_Filter == null || q.PriorityWeight >= parameters.MinWeight_Filter) &&
            (parameters.MaxWeight_Filter == null || q.PriorityWeight <= parameters.MaxWeight_Filter));
        
        return await GetPagedAsync(baseQuery, parameters);
    }
}