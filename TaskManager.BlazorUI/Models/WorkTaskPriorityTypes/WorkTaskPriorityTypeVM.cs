using System.ComponentModel.DataAnnotations;

namespace TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;

public class WorkTaskPriorityTypeVM
{
    public int Id { get; set; }

    [Display (Name = "Priority Type Name")]
    [Required]
    public string Name { get; set; }

    [Display(Name = "Priority Weight")]
    [Range(0, 1000)]
    public int PriorityWeight { get; set; }
}
