using Microsoft.AspNetCore.Components;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;

namespace TaskManager.BlazorUI.Pages.WorkTaskPriorityTypes
{
    public partial class Details
    {
        [Inject]
        public IWorkTaskPriorityTypeService WorkTaskPriorityTypeService { get; set; }
        [Inject]
        public INavigationService NavigationService { get; set; }
        public string Message { get; set; } = string.Empty;
        public WorkTaskPriorityTypeVM Model { get; set; }

        [Parameter]
        public int id { get; set; }


        protected async override Task OnParametersSetAsync()
        {
            try
            {
                Model = await WorkTaskPriorityTypeService.GetWorkTaskPriorityTypeDetails(id);
                if (Model == null)
                {
                    Message = "Priority Type Not Found";
                }
            }
            catch
            {
                Message = "Something went wrong";
            }
        }
    }
}