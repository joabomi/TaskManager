using TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetAllWorkTaskPriorityTypes;
using TaskManager.Domain;
using TaskManager.Domain.Common;

namespace TaskManager.Application.Contracts.Persistence
{
    public interface IWorkTaskPriorityTypeRepository : IGenericRepository<WorkTaskPriorityType>
    {
        public Task<bool> IsWorkPriorityTypeUnique(string name, int priorityWeight);

        public Task<bool> IsWorkPriorityTypeUpdateValid(string name, int weight, int id);

        public Task<PagedResult<WorkTaskPriorityType>> GetPagedAsync(GetAllWorkTaskPriorityTypesQuery query);
    }
}
