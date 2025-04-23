using System.ComponentModel.DataAnnotations;

namespace TaskManager.BlazorUI.Models.WorkTasks;

public class WorkTaskVM
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}
