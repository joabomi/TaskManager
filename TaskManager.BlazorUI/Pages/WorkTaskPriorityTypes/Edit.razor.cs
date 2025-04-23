using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;

namespace TaskManager.BlazorUI.Pages.WorkTaskPriorityTypes;

public partial class Edit
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    [Inject]
    public IWorkTaskPriorityTypeService WorkTaskPriorityTypeService { get; set; }
    public WorkTaskPriorityTypeVM Model { get; set; } = new WorkTaskPriorityTypeVM();
    public string WeightString { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    [Parameter]
    public int id { get; set; }

    protected async override Task OnInitializedAsync()
    {
        Model = await WorkTaskPriorityTypeService.GetWorkTaskPriorityTypeDetails(id);
        WeightString = Model.PriorityWeight.ToString();
    }

    [Authorize]
    public async Task EditPriorityType()
    {
        Message = string.Empty;
        if (string.IsNullOrEmpty(Model.Name))
        {
            Message = "Priority Type Name is required.";
            return;
        }
        if (string.IsNullOrEmpty(WeightString))
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
        var result = await WorkTaskPriorityTypeService.UpdateWorkTaskPriorityType(id, Model);
        if (result.Success)
        {
            GoBack();
        }
        else
        {
            Message = "Something went wrong.";
        }
    }

    public void GoBack()
    {
        NavigationManager.NavigateTo("/prioritytypes");
    }
}
