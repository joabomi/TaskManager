using System.ComponentModel.DataAnnotations;

namespace TaskManager.BlazorUI.Models.WorkTaskStatusTypes;

public class WorkTaskPriorityTypeVM
{
    public class WorkTaskStatusTypeVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
