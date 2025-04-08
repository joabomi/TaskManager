using TaskManager.Domain;

namespace TaskManager.Application.Contracts.Persistence
{
    public interface IWorkTaskPriorityTypeRepository : IGenericRepository<WorkTaskPriorityType>
    {
        public Task<bool> IsWorkPriorityTypeUnique(string name);
    }
}
