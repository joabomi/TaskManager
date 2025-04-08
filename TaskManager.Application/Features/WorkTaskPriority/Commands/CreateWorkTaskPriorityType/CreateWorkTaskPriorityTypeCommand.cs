using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TaskManager.Application.Features.WorkTaskPriority.Commands.CreateWorkTaskPriorityType;

public class CreateWorkTaskPriorityTypeCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public int PriorityWeight { get; set; } = 0;//Higher values indicate higher priority.
}
