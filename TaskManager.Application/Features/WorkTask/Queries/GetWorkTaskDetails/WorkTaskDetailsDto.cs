using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetAllWorkTaskPriorityTypes;
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetAllWorkTaskStatusTypes;
using TaskManager.Domain;

namespace TaskManager.Application.Features.WorkTask.Queries.GetWorkTaskDetails;

public class WorkTaskDetailsDto
{
    public int Id { get; set; } = -1;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public WorkTaskStatusTypeDto? Status { get; set; }
    public int StatusId { get; set; }
    public WorkTaskPriorityTypeDto? Priority { get; set; }
    public int PriorityId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string AssignedPersonId { get; set; } = string.Empty;
}
