using global::TaskManager.BlazorUI.Contracts;
using Microsoft.AspNetCore.Components;
using TaskManager.BlazorUI.Models.WorkTasks;
using TaskManager.BlazorUI.Services;

namespace TaskManager.BlazorUI.Pages.WorkTasks;

public partial class Index
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IWorkTaskService WorkTaskService { get; set; }
    public List<WorkTaskVM> WorkTasks { get; private set; }

    [Inject]
    public IUserService UserService { get; set; }
    public Dictionary<string, string> UserNamesById { get; set; } = new Dictionary<string, string>();
    public string Message { get; set; } = string.Empty;
    private bool _canDelete { get; set; } = false;
    protected void CreateWorkTask()
    {
        NavigationManager.NavigateTo("/worktasks/create");
    }
    protected void EditWorkTask(int id)
    {
        NavigationManager.NavigateTo($"/worktasks/edit/{id}");

    }
    protected void DetailsWorkTask(int id)
    {
        NavigationManager.NavigateTo($"/worktasks/details/{id}");

    }
    protected async Task DeleteWorkTask(int id)
    {
        var response = await WorkTaskService.DeleteWorkTask(id);
        if (response.Success)
        {
            WorkTasks.RemoveAll(WorkTasks => WorkTasks.Id == id);
            StateHasChanged();
        }
        else
        {
            Message = response.Message;
        }
    }

    protected override async Task OnInitializedAsync()
    {
        WorkTasks = await WorkTaskService.GetWorkTasks();

        var users = await UserService.GetUsers();
        UserNamesById = users.ToDictionary(user => user.Id, user => $"{user.FirstName} {user.LastName}");

        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        if (user.Identity.IsAuthenticated)
        {
            _canDelete = user.IsInRole("Administrator");
        }

    }
}