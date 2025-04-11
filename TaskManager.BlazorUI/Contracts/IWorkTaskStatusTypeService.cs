using TaskManager.BlazorUI.Models.WorkTaskStatusTypes;
using TaskManager.BlazorUI.Services.Base;

namespace TaskManager.BlazorUI.Contracts;

public interface IWorkTaskStatusTypeService
{
    Task<List<WorkTaskStatusTypeVM>> GetWorkTaskStatusTypes();
    Task<WorkTaskStatusTypeVM> GetWorkTaskStatusTypeDetails(int id);
    Task<Response<int>> CreateWorkTaskStatusType(WorkTaskStatusTypeVM workTaskStatusType);
    Task<Response<Guid>> UpdateWorkTaskStatusType(int id, WorkTaskStatusTypeVM workTaskStatusType);
    Task<Response<Guid>> DeleteWorkTaskStatusType(int id);
}
