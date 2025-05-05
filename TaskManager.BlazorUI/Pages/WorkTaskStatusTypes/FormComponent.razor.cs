using Microsoft.AspNetCore.Components;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models.WorkTaskStatusTypes;

namespace TaskManager.BlazorUI.Pages.WorkTaskStatusTypes
{
    public partial class FormComponent
    {
        [Inject] private INavigationService _navigationService { get; set; } = default!;
        [Parameter] public bool Disabled { get; set; } = false;
        [Parameter] public WorkTaskStatusTypeVM StatusType { get; set; }
        [Parameter] public string ButtonText { get; set; } = "Save";
        [Parameter] public EventCallback OnValidSubmit { get; set; }
    }
}