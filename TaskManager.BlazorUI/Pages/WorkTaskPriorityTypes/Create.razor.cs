using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;

namespace TaskManager.BlazorUI.Pages.WorkTaskPriorityTypes
{
    public partial class Create
    {
        [Inject]
        public INavigationService NavigationService { get; set; }

        [Inject]
        public IWorkTaskPriorityTypeService WorkTaskPriorityTypeService { get; set; }

        internal WorkTaskPriorityTypeVM priorityType { get; set; } = new WorkTaskPriorityTypeVM();

        public string Message { get; set; } = string.Empty;

        protected override void OnInitialized()
        {
            priorityType = new WorkTaskPriorityTypeVM();
        }

        [Authorize]
        public async Task CreateNewType()
        {

            var result = await WorkTaskPriorityTypeService.CreateWorkTaskPriorityType(priorityType);

            if (result.Success)
            {
                NavigationService.GoBack();
            }
            else
            {
                Message = "Failed to create Priority Type.";
            }
        }
    }
}