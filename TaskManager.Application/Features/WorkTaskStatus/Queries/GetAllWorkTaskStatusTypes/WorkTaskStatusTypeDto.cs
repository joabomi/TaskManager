using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Features.WorkTaskStatus.Queries.GetAllWorkTaskStatusTypes
{
    public class WorkTaskStatusTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
