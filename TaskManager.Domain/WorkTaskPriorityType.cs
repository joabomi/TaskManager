using TaskManager.Domain.Common;

namespace TaskManager.Domain;

public class WorkTaskPriorityType: BaseEntity
{
    public string Name {  get; set; } = string.Empty;
    public int PriorityWeight { get; set; }
}