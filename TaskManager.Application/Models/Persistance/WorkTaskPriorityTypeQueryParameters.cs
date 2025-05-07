using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Models.Persistance;

public class WorkTaskPriorityTypeQueryParameters : BaseQueryParameters
{
    public string? Name_Filter { get; set; }
    public int? MinWeight_Filter { get; set; }
    public int? MaxWeight_Filter { get; set; }
}
