using Microsoft.AspNetCore.Components;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models.WorkTaskStatusTypes;

namespace TaskManager.BlazorUI.Pages.WorkTaskStatusTypes
{
    public partial class Details
    {
        [Inject]
        public IWorkTaskStatusTypeService WorkTaskStatusTypeService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string Message { get; set; } = string.Empty;
        public WorkTaskStatusTypeVM Model { get; set; }

        [Parameter]
        public int id { get; set; }


        protected async override Task OnParametersSetAsync()
        {
            try
            {
                Model = await WorkTaskStatusTypeService.GetWorkTaskStatusTypeDetails(id);
                if (Model == null)
                {
                    Message = "Status Type Not Found";
                }
            }
            catch
            {
                Message = "Something went wrong";
            }
        }

        public void GoBack()
        {
            NavigationManager.NavigateTo("/statustypes");
        }
    }
}