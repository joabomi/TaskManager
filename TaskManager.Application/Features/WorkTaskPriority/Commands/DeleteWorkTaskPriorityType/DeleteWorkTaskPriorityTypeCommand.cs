using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TaskManager.Application.Features.WorkTaskPriority.Commands.DeleteWorkTaskPriorityType;

public class DeleteWorkTaskPriorityTypeCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
