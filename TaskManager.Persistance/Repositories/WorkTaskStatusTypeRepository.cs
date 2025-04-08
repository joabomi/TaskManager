using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Domain;
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
}