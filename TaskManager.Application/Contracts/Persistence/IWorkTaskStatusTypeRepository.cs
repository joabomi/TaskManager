using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetAllWorkTaskStatusTypes;
using TaskManager.Domain;
using TaskManager.Domain.Common;

namespace TaskManager.Application.Contracts.Persistence
{
    public interface IWorkTaskStatusTypeRepository : IGenericRepository<WorkTaskStatusType>
    {
        public Task<bool> IsWorkStatusTypeUnique(string name);
        public Task<PagedResult<WorkTaskStatusType>> GetPagedAsync(GetAllWorkTaskStatusTypesQuery query);
    }
}
