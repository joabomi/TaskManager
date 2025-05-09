using TaskManager.Domain.Common;

namespace TaskManager.Domain;

public class WorkTask : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public WorkTaskStatusType? Status { get; set; }
    public int StatusId { get; set; }
    public WorkTaskPriorityType? Priority { get; set; }
    public int PriorityId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string AssignedPersonId { get; set; } = string.Empty;
}
