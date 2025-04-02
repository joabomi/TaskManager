using TaskManager.Domain.Common;

namespace TaskManager.Domain;

public class WorkTaskStatusType: BaseEntity
{
    public string Name { get; set; } = string.Empty;
}