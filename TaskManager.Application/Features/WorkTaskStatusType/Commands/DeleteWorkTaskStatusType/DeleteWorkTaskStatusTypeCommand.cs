using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TaskManager.Application.Features.WorkTaskStatusType.Commands.DeleteWorkTaskStatusType;

public class DeleteWorkTaskStatusTypeCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
