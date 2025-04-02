using TaskManager.Domain.Common;

namespace TaskManager.Domain;

public class WorkTaskPriority: BaseEntity
{
    public string Name {  get; set; } = string.Empty;
    public int PriorityWeight { get; set; }
}