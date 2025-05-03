using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models.WorkTaskStatusTypes;

namespace TaskManager.BlazorUI.Pages.WorkTaskStatusTypes;

public partial class Edit
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    [Inject]
    public IWorkTaskStatusTypeService WorkTaskStatusTypeService { get; set; }
    internal WorkTaskStatusTypeVM statusType { get; set; } = new WorkTaskStatusTypeVM();
    public string Message { get; set; } = string.Empty;

    [Parameter]
    public int id { get; set; }

    protected async override Task OnInitializedAsync()
    {
        statusType = await WorkTaskStatusTypeService.GetWorkTaskStatusTypeDetails(id);
    }

    [Authorize]
    public async Task EditStatusType()
    {
        var result = await WorkTaskStatusTypeService.UpdateWorkTaskStatusType(id, statusType);
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
        NavigationManager.NavigateTo("/statustypes");
    }
}
