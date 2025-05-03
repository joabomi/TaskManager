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
        public string Message { get; set; } = string.Empty;
        internal WorkTaskStatusTypeVM statusType { get; set; } = new WorkTaskStatusTypeVM();

        protected override void OnInitialized()
        {
            statusType = new WorkTaskStatusTypeVM();
        }

        [Authorize]
        public async Task CreateNewType()
        {
            var result = await WorkTaskStatusTypeService.CreateWorkTaskStatusType(statusType);
            if (result.Success)
            {
                GoBack();
            }
            else
            {
                Message = "Failed to create Status Type.";
                StateHasChanged();
            }

        }

        public void GoBack()
        {
            NavigationManager.NavigateTo("/statustypes");
        }
    }
}