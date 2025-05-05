using Microsoft.AspNetCore.Components;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;

namespace TaskManager.BlazorUI.Pages.WorkTaskPriorityTypes
{
    public partial class FormComponent
    {
        [Inject] private INavigationService _navigationService { get; set; }
        [Parameter] public bool Disabled { get; set; } = false;
        [Parameter] public WorkTaskPriorityTypeVM PriorityType { get; set; }
        [Parameter] public string ButtonText { get; set; } = "Save";
        [Parameter] public EventCallback OnValidSubmit { get; set; }
    }
}