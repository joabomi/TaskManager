using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TaskManager.Application.Features.WorkTask.Commands.DeleteWorkTask;

public class DeleteWorkTaskCommand : IRequest<Unit>
{
    public int Id { get; set; } = -1;
}
