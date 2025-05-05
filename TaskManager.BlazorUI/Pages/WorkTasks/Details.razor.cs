using Microsoft.AspNetCore.Components;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models;
using TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;
using TaskManager.BlazorUI.Models.WorkTasks;
using TaskManager.BlazorUI.Models.WorkTaskStatusTypes;

namespace TaskManager.BlazorUI.Pages.WorkTasks
{
    public partial class Details
    {
        [Inject]
        public IWorkTaskService WorkTaskService { get; set; }

        [Inject]
        public INavigationService NavigationService { get; set; }

        public string Message { get; set; } = string.Empty;

        public WorkTaskVM WorkTask { get; set; }

        [Inject]
        public IWorkTaskStatusTypeService WorkTaskStatusTypeService { get; set; }

        [Inject]
        public IWorkTaskPriorityTypeService WorkTaskPriorityTypeService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        internal UserVM user { get; set; } = new UserVM();

        [Parameter]
        public int Id { get; set; }


        protected async override Task OnParametersSetAsync()
        {
            try
            {
                WorkTask = await WorkTaskService.GetWorkTaskDetails(Id);
                if (WorkTask == null)
                {
                    Message = "Work Task Not Found";
                }
                else
                {
                    WorkTask.Status = await WorkTaskStatusTypeService.GetWorkTaskStatusTypeDetails(WorkTask.StatusId);
                    WorkTask.Priority = await WorkTaskPriorityTypeService.GetWorkTaskPriorityTypeDetails(WorkTask.PriorityId);
                    user = await UserService.GetUser(WorkTask.AssignedPersonId);
                }
            }
            catch
            {
                Message = "Something went wrong";
            }
        }
    }
}