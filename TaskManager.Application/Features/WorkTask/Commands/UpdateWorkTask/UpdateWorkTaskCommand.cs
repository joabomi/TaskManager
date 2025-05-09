using MediatR;
using TaskManager.Application.Features.WorkTask.Shared;

namespace TaskManager.Application.Features.WorkTask.Commands.UpdateWorkTask;

public class UpdateWorkTaskCommand : BaseWorkTaskCommand, IRequest<Unit>
{
    public int Id { get; set; } = -1;
}
