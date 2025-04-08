using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Features.WorkTaskPriority.Queries.GetAllWorkTaskPriorityTypes;
using TaskManager.Application.Features.WorkTaskStatus.Queries.GetAllWorkTaskStatusTypes;
using TaskManager.Domain;

namespace TaskManager.Application.Features.WorkTask.Queries.GetAllWorkTasks;

public class WorkTaskDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public WorkTaskStatusTypeDto? Status { get; set; }
    public int StatusId { get; set; }
    public WorkTaskPriorityTypeDto? Priority { get; set; }
    public int PriorityId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string AssignedEmployeeId { get; set; } = string.Empty;
}
