using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManager.Application.Features.WorkTask.Shared;
using TaskManager.Domain;

namespace TaskManager.Application.Features.WorkTask.Commands.UpdateWorkTask;

public class UpdateWorkTaskCommand : BaseWorkTaskCommand, IRequest<Unit>
{
    public int Id { get; set; }
    public string AssignedPersonId { get; set; } = string.Empty;
}
