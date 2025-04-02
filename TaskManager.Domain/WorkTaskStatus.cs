using TaskManager.Domain.Common;

namespace TaskManager.Domain;

public class WorkTaskStatus: BaseEntity
{
    public string Name { get; set; } = string.Empty;
}