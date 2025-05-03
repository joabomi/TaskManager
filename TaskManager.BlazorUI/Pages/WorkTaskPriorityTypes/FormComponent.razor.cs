using Microsoft.AspNetCore.Components;
using TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;

namespace TaskManager.BlazorUI.Pages.WorkTaskPriorityTypes
{
    public partial class FormComponent
    {
        [Parameter] public bool Disabled { get; set; } = false;
        [Parameter] public WorkTaskPriorityTypeVM PriorityType { get; set; }
        [Parameter] public string ButtonText { get; set; } = "Save";
        [Parameter] public EventCallback OnValidSubmit { get; set; }

    }
}