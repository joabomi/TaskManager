using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;

namespace TaskManager.BlazorUI.Pages.WorkTaskPriorityTypes;

public partial class Edit
{
    [Inject]
    public INavigationService NavigationService { get; set; }
    [Inject]
    public IWorkTaskPriorityTypeService WorkTaskPriorityTypeService { get; set; }
    internal WorkTaskPriorityTypeVM priorityType { get; set; } = new WorkTaskPriorityTypeVM();
    public string Message { get; set; } = string.Empty;

    [Parameter]
    public int id { get; set; }

    protected async override Task OnInitializedAsync()
    {
        priorityType = await WorkTaskPriorityTypeService.GetWorkTaskPriorityTypeDetails(id);
    }

    [Authorize]
    public async Task EditPriorityType()
    {
        var result = await WorkTaskPriorityTypeService.UpdateWorkTaskPriorityType(id, priorityType);
        if (result.Success)
        {
            NavigationService.GoBack();
        }
        else
        {
            Message = "Something went wrong.";
        }
    }
}
