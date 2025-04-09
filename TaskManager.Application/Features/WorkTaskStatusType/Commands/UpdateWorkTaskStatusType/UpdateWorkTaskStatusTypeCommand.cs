using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TaskManager.Application.Features.WorkTaskStatusType.Commands.UpdateWorkTaskStatusType;

public class UpdateWorkTaskStatusTypeCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
