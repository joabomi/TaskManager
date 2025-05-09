using MediatR;
using TaskManager.Application.Features.WorkTask.Shared;

namespace TaskManager.Application.Features.WorkTask.Commands.CreateWorkTask;

public class CreateWorkTaskCommand : BaseWorkTaskCommand, IRequest<int>
{
}
