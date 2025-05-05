using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models;
using TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;
using TaskManager.BlazorUI.Models.WorkTasks;
using TaskManager.BlazorUI.Models.WorkTaskStatusTypes;

namespace TaskManager.BlazorUI.Pages.WorkTasks
{
    public partial class Create
    {
        [Inject] 
        public INavigationService NavigationService { get; set; }

        [Inject]
        public IWorkTaskService WorkTaskService { get; set; }

        internal WorkTaskVM workTask { get; set; } = new WorkTaskVM();

        [Inject]
        public IWorkTaskStatusTypeService WorkTaskStatusTypeService { get; set; }

        internal List<WorkTaskStatusTypeVM> workTaskStatusTypes { get; set; } = new List<WorkTaskStatusTypeVM>();

        [Inject]
        public IWorkTaskPriorityTypeService WorkTaskPriorityTypeService { get; set; }
        internal List<WorkTaskPriorityTypeVM> workTaskPriorityTypes { get; set; } = new List<WorkTaskPriorityTypeVM>();

        [Inject]
        public IUserService UserService { get; set; }
        internal List<UserVM> users { get; set; } = new List<UserVM>();

        public string Message { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            workTaskStatusTypes = await WorkTaskStatusTypeService.GetWorkTaskStatusTypes();
            workTaskPriorityTypes = await WorkTaskPriorityTypeService.GetWorkTaskPriorityTypes();
            users = await UserService.GetUsers();
        }

        [Authorize]
        public async Task CreateTask()
        {

            var result = await WorkTaskService.CreateWorkTask(workTask);
            if (result.Success)
            {
                NavigationService.GoBack();
            }
            else
            {
                Message = "Failed to create Task.";
            }
        }
    }
}