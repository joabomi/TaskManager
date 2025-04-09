using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetWorkTaskPriorityDetails;

public class WorkTaskPriorityTypeDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PriorityWeight { get; set; } = 0;//Higher values indicate higher priority.
    public DateTime? CreationDate { get; set; }
    public DateTime? LastModificationDate { get; set; }
}
