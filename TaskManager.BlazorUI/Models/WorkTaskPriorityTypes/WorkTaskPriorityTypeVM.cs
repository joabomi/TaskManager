using System.ComponentModel.DataAnnotations;

namespace TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;

public class WorkTaskPriorityTypeVM
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int PriorityWeight { get; set; }
}
