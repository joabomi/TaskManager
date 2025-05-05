using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models;
using TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;
using TaskManager.BlazorUI.Models.WorkTasks;
using TaskManager.BlazorUI.Models.WorkTaskStatusTypes;

namespace TaskManager.BlazorUI.Pages.WorkTasks;

public partial class FormComponent
{
    [Inject] private INavigationService _navigationService { get; set; }
    [Inject] private AuthenticationStateProvider _authenticationStateProvider { get; set; }
    [Parameter] public bool Disabled { get; set; } = false;
    [Parameter] public WorkTaskVM WorkTask { get; set; } = new();
    [Parameter] public string ButtonText { get; set; } = "Save";
    [Parameter] public EventCallback OnValidSubmit { get; set; }
    [Parameter] public List<WorkTaskStatusTypeVM> WorkTaskStatusTypes { get; set; } = new List<WorkTaskStatusTypeVM>();
    [Parameter] public List<WorkTaskPriorityTypeVM> WorkTaskPriorityTypes { get; set; } = new List<WorkTaskPriorityTypeVM>();
    [Parameter] public List<UserVM> Users { get; set; } = new List<UserVM>();

    private bool IsAdmin { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        IsAdmin = user.IsInRole("Administrator");
    }
}