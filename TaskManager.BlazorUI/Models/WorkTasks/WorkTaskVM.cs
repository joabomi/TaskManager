using System.ComponentModel.DataAnnotations;
using TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;
using TaskManager.BlazorUI.Models.WorkTaskStatusTypes;

namespace TaskManager.BlazorUI.Models.WorkTasks;

public class WorkTaskVM
{
    public int Id { get; set; }

    [Display(Name = "Task Name")]
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Display(Name = "Task Description")]
    [MaxLength(500)]
    public string Description { get; set; }

    public WorkTaskStatusTypeVM Status { get; set; }

    [Display(Name = "Task Status")]
    [Required]
    public int StatusId { get; set; }

    public WorkTaskPriorityTypeVM Priority { get; set; }

    [Display(Name = "Task Priority")]
    [Required]
    public int PriorityId { get; set; }

    [Display(Name = "Task Start Date")]
    [Required]
    public DateTime StartDate { get; set; } = DateTime.Now.Date;

    [Display(Name = "Task End Date")]
    [Required]
    public DateTime EndDate { get; set; } = DateTime.Now.Date;

    public string AssignedPersonId { get; set; }
}
