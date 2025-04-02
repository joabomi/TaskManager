using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TaskManager.Application.Features.WorkTaskStatus.Command.DeleteWorkStatusType;

public class DeleteWorkStatusTypeCommand : IRequest<Unit>
{
    public string Name { get; set; } = string.Empty;
}
