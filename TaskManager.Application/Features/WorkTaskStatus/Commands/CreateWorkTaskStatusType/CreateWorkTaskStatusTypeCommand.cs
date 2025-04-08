using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TaskManager.Application.Features.WorkTaskStatus.Commands.CreateWorkTaskStatusType;

public class CreateWorkTaskStatusTypeCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
}
