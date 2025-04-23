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
    public WorkTaskStatusTypeVM Model { get; set; } = new WorkTaskStatusTypeVM();
    public string Message { get; set; } = string.Empty;

    [Parameter]
    public int id { get; set; }

    protected async override Task OnInitializedAsync()
    {
        Model = await WorkTaskStatusTypeService.GetWorkTaskStatusTypeDetails(id);
    }

    [Authorize]
    public async Task EditStatusType()
    {
        Message = string.Empty;
        if (string.IsNullOrEmpty(Model.Name))
        {
            Message = "Status Type Name is required.";
            return;
        }
        var result = await WorkTaskStatusTypeService.UpdateWorkTaskStatusType(id, Model);
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
