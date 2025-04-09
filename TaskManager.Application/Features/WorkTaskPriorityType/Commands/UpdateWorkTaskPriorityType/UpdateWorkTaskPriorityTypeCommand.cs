using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TaskManager.Application.Features.WorkTaskPriorityType.Commands.UpdateWorkTaskPriorityType;

public class UpdateWorkTaskPriorityTypeCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PriorityWeight { get; set; } = 0;//Higher values indicate higher priority.
}
