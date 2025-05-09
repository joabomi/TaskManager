using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetAllWorkTaskStatusTypes;
using TaskManager.Domain;
using TaskManager.Domain.Common;
using TaskManager.Persistance.DatabaseContext;

namespace TaskManager.Persistance.Repositories;

public class WorkTaskStatusTypeRepository : GenericRepository<WorkTaskStatusType>, IWorkTaskStatusTypeRepository
{
    public WorkTaskStatusTypeRepository(TaskManagerDatabaseContext context) : base(context)
    {
    }

    public async Task<bool> IsWorkStatusTypeUnique(string name)
    {
        return !await _context.WorkTaskStatusTypes.AnyAsync(q => q.Name == name);
    }

    public async Task<PagedResult<WorkTaskStatusType>> GetPagedAsync(GetAllWorkTaskStatusTypesQuery query)
    {
        var baseQuery = _context.WorkTaskStatusTypes.AsQueryable();

        baseQuery = baseQuery.Where(q => string.IsNullOrEmpty(query.Name_Filter) || q.Name.Contains(query.Name_Filter));

        return await GetPagedAsync(baseQuery, query);
    }
}