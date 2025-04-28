using System.ComponentModel.DataAnnotations;
using TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;
using TaskManager.BlazorUI.Models.WorkTaskStatusTypes;

namespace TaskManager.BlazorUI.Models.WorkTasks;

public class WorkTaskDetailsVM
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public WorkTaskStatusTypeVM Status { get; set; }
    [Required]
    public int StatusId { get; set; }
    [Required]
    public WorkTaskPriorityTypeVM Priority { get; set; }
    public int PriorityId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string AssignedPersonId { get; set; }
}
