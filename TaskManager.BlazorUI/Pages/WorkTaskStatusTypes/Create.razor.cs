using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models.WorkTaskStatusTypes;

namespace TaskManager.BlazorUI.Pages.WorkTaskStatusTypes
{
    public partial class Create
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IWorkTaskStatusTypeService WorkTaskStatusTypeService { get; set; }
        public WorkTaskStatusTypeVM Model { get; set; } = new WorkTaskStatusTypeVM();
        public string Message { get; set; } = string.Empty;

        protected override void OnInitialized()
        {
            Model = new WorkTaskStatusTypeVM();
        }

        [Authorize]
        public async void CreateNewType()
        {
            Message = string.Empty;
            if (string.IsNullOrEmpty(Model.Name))
            {
                Message = "Status Type Name is required.";
                return;
            }
            var result = await WorkTaskStatusTypeService.CreateWorkTaskStatusType(Model);
            if (result.Success)
            {
                NavigationManager.NavigateTo("/statustypes");
            }
            else
            {
                Message = "Failed to create Status Type.";
            }
        }
    }
}