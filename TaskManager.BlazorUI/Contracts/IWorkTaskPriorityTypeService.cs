using TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;
using TaskManager.BlazorUI.Services.Base;

namespace TaskManager.BlazorUI.Contracts;

public interface IWorkTaskPriorityTypeService
{
    Task<List<WorkTaskPriorityTypeVM>> GetWorkTaskPriorityTypes();
    Task<WorkTaskPriorityTypeVM> GetWorkTaskPriorityTypeDetails(int id);
    Task<Response<int>> CreateWorkTaskPriorityType(WorkTaskPriorityTypeVM workTaskPriorityType);
    Task<Response<Guid>> UpdateWorkTaskPriorityType(int id, WorkTaskPriorityTypeVM workTaskPriorityType);
    Task<Response<Guid>> DeleteWorkTaskPriorityType(int id);
}
