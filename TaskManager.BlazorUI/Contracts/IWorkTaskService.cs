using TaskManager.BlazorUI.Models.WorkTasks;
using TaskManager.BlazorUI.Services.Base;

namespace TaskManager.BlazorUI.Contracts;

public interface IWorkTaskService
{
    Task<List<WorkTaskVM>> GetWorkTasks();
    Task<WorkTaskVM> GetWorkTaskDetails(int id);
    Task<Response<int>> CreateWorkTask(WorkTaskVM workTask);
    Task<Response<Guid>> UpdateWorkTask(int id, WorkTaskVM workTask);
    Task<Response<Guid>> DeleteWorkTask(int id);
}
