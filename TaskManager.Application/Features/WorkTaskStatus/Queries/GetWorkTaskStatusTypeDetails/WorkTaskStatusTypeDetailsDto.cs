using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Features.WorkTaskStatus.Queries.GetWorkTaskStatusTypeDetails;

public class WorkTaskStatusTypeDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime? CreationDate { get; set; }
    public DateTime? LastModificationDate { get; set; }
}
