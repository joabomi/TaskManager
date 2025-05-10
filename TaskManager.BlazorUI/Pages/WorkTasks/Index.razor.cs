using global::TaskManager.BlazorUI.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using TaskManager.BlazorUI.Models;
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
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        Console.WriteLine($"User: {user.Identity.Name}");
        if (user.Identity.IsAuthenticated)
        {
            bool isAdmin = user.IsInRole("Administrator");
            Console.WriteLine($"Is Admin: {isAdmin}");
            bool isUser = user.IsInRole("TaskManagerUser");
            Console.WriteLine($"Is User: {isUser}");
            _canDelete = isAdmin;
            WorkTasks = await WorkTaskService.GetWorkTasks();
            if (isAdmin)
            {

                var users = await UserService.GetUsers();
                UserNamesById = users.ToDictionary(user => user.Id, user => $"{user.FirstName} {user.LastName}");
            }
            else if (isUser)
            {
                var id = user.Claims.Where(c => c.Type == "uid").FirstOrDefault()?.Value;
                if(!string.IsNullOrEmpty(id))
                {
                    var users = await UserService.GetUsers();
                    UserNamesById = users.Where(u => u.Id == id).ToDictionary(user => user.Id, user => $"{user.FirstName} {user.LastName}");
                }
            }
            else
            {
                WorkTasks = new List<WorkTaskVM>();
            }
        }

    }
}