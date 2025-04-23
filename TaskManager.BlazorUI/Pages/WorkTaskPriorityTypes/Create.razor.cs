using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;

namespace TaskManager.BlazorUI.Pages.WorkTaskPriorityTypes
{
    public partial class Create
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IWorkTaskPriorityTypeService WorkTaskPriorityTypeService { get; set; }
        public WorkTaskPriorityTypeVM Model { get; set; } = new WorkTaskPriorityTypeVM();
        public string WeightString { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

        protected override void OnInitialized()
        {
            Model = new WorkTaskPriorityTypeVM();
        }

        [Authorize]
        public async void CreateNewType()
        {
            Message = string.Empty;
            if (string.IsNullOrEmpty(Model.Name))
            {
                Message = "Priority Type Name is required.";
                return;
            }
            if(string.IsNullOrEmpty(WeightString))
            {
                Message = "Priority Weight is required.";
                return;
            }
            bool parse_result = int.TryParse(WeightString, out int parsedWeight);
            if (!parse_result)
            {
                Message = "Priority Weight must be an integer number.";
                return;
            }
            else
            {
                Model.PriorityWeight = parsedWeight;
            }

            var result = await WorkTaskPriorityTypeService.CreateWorkTaskPriorityType(Model);

            if (result.Success)
            {
                NavigationManager.NavigateTo("/prioritytypes");
            }
            else
            {
                Message = "Failed to create Priority Type.";
            }
        }

        public void GoBack()
        {
            NavigationManager.NavigateTo("/prioritytypes");
        }
    }
}