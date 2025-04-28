using global::TaskManager.BlazorUI.Contracts;
using Microsoft.AspNetCore.Components;
using TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;

namespace TaskManager.BlazorUI.Pages.WorkTaskPriorityTypes;

public partial class Index
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IWorkTaskPriorityTypeService WorkTaskPriorityTypeService { get; set; }
    public List<WorkTaskPriorityTypeVM> WorkTaskPriorityTypes { get; private set; }
    public string Message { get; set; } = string.Empty;
    private bool _canDelete { get; set; } = false;
    protected void CreateWorkTaskPriorityType()
    {
        NavigationManager.NavigateTo("/prioritytypes/create");
    }
    protected void EditPriorityType(int id)
    {
        NavigationManager.NavigateTo($"/prioritytypes/edit/{id}");

    }
    protected void DetailsPriorityType(int id)
    {
        NavigationManager.NavigateTo($"/prioritytypes/details/{id}");

    }
    protected async Task DeletePriorityType(int id)
    {
        var response = await WorkTaskPriorityTypeService.DeleteWorkTaskPriorityType(id);
        if (response.Success)
        {
            WorkTaskPriorityTypes.RemoveAll(WorkTaskPriorityTypes => WorkTaskPriorityTypes.Id == id);
            StateHasChanged();
        }
        else
        {
            Message = response.Message;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        WorkTaskPriorityTypes = await WorkTaskPriorityTypeService.GetWorkTaskPriorityTypes();

        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        if (user.Identity.IsAuthenticated)
        {
            _canDelete = user.IsInRole("Administrator");
        }

    }
}