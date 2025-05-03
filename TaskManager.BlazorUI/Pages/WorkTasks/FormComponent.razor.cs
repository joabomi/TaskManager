using Microsoft.AspNetCore.Components;
using TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetAllWorkTaskPriorityTypes;
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetAllWorkTaskStatusTypes;
using TaskManager.BlazorUI.Models;
using TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;
using TaskManager.BlazorUI.Models.WorkTasks;
using TaskManager.BlazorUI.Models.WorkTaskStatusTypes;

namespace TaskManager.BlazorUI.Pages.WorkTasks;

public partial class FormComponent
{
    [Inject] private NavigationManager _navigationManager { get; set; } = default!;
    [Parameter] public bool Disabled { get; set; } = false;
    [Parameter] public WorkTaskVM WorkTask { get; set; } = new();
    [Parameter] public string ButtonText { get; set; } = "Save";
    [Parameter] public EventCallback OnValidSubmit { get; set; }
    [Parameter] public List<WorkTaskStatusTypeVM> WorkTaskStatusTypes { get; set; } = new List<WorkTaskStatusTypeVM>();
    [Parameter] public List<WorkTaskPriorityTypeVM> WorkTaskPriorityTypes { get; set; } = new List<WorkTaskPriorityTypeVM>();
    [Parameter] public List<UserVM> Users { get; set; } = new List<UserVM>();

    public void GoBack()
    {
        _navigationManager.NavigateTo("/worktasks");
    }
}