using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetAllWorkTaskPriorityTypes;
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

    public async Task<PagedResult<WorkTaskPriorityType>> GetPagedAsync(GetAllWorkTaskPriorityTypesQuery query)
    {
        var baseQuery = _context.WorkTaskPriorityTypes.AsQueryable();

        baseQuery = baseQuery.Where(q =>
            (string.IsNullOrEmpty(query.Name_Filter) || q.Name.Contains(query.Name_Filter)) &&
            (query.MinWeight_Filter == null || q.PriorityWeight >= query.MinWeight_Filter) &&
            (query.MaxWeight_Filter == null || q.PriorityWeight <= query.MaxWeight_Filter));

        return await GetPagedAsync(baseQuery, query);
    }
}