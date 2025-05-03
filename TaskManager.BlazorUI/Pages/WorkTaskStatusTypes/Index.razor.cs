using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models.WorkTaskStatusTypes;

namespace TaskManager.BlazorUI.Pages.WorkTaskStatusTypes;

public partial class Index
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IWorkTaskStatusTypeService WorkTaskStatusTypeService { get; set; }
    public List<WorkTaskStatusTypeVM> WorkTaskStatusTypes { get; private set; }
    public string Message { get; set; } = string.Empty;
    private bool _isAdmin { get; set; } = false;

    protected void CreateWorkTaskStatusType()
    {
        NavigationManager.NavigateTo("/statustypes/create");
    }
    protected void EditStatusType(int id)
    {
        NavigationManager.NavigateTo($"/statustypes/edit/{id}");

    }
    protected void DetailsStatusType(int id)
    {
        NavigationManager.NavigateTo($"/statustypes/details/{id}");

    }
    protected async Task DeleteStatusType(int id)
    {
        var response = await WorkTaskStatusTypeService.DeleteWorkTaskStatusType(id);
        if (response.Success)
        {
            WorkTaskStatusTypes.RemoveAll(WorkTaskStatusTypes => WorkTaskStatusTypes.Id == id);
            StateHasChanged();
        }
        else
        {
            Message = response.Message;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        WorkTaskStatusTypes = await WorkTaskStatusTypeService.GetWorkTaskStatusTypes();

        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        if (user.Identity.IsAuthenticated)
        {
            _isAdmin = user.IsInRole("Administrator");
        }

    }
}