using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Features.WorkTask.Shared;

public abstract class BaseWorkTaskCommand
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int StatusId { get; set; }
    public int PriorityId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
