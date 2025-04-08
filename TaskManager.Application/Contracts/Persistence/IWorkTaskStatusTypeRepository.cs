using TaskManager.Domain;

namespace TaskManager.Application.Contracts.Persistence
{
    public interface IWorkTaskStatusTypeRepository : IGenericRepository<WorkTaskStatusType>
    {
        public Task<bool> IsWorkStatusTypeUnique(string name);
    }
}
