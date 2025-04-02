using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Common;

namespace TaskManager.Domain;

public class WorkTask : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public WorkTaskStatus? Status { get; set; }
    public WorkTaskPriority? Priority { get; set; }
    public DateTime LimitDate { get; set; }

    //public TaskUser User { get; set; }
    //public bool Asigned { get; set; }
}
