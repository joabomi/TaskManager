using TaskManager.Domain;

namespace TaskManager.Application.Contracts.Persistence
{
    public interface IWorkTaskPriorityTypeRepository : IGenericRepository<WorkTaskPriorityType>
    {
        public Task<bool> IsWorkPriorityTypeUnique(string name, int priorityWeight);

        public Task<bool> IsWorkPriorityTypeUpdateValid(string name, int weight, int id);
    }
}
