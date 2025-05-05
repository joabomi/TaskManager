using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Domain;
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
}