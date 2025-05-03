using System.ComponentModel.DataAnnotations;

namespace TaskManager.BlazorUI.Models.WorkTaskStatusTypes
{
    public class WorkTaskStatusTypeVM
    {
        public int Id { get; set; }

        [Display(Name = "Status Type Name")]
        [Required]
        public string Name { get; set; }
    }
}
