using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTask.Shared;

namespace TaskManager.Application.Features.WorkTask.Commands.UpdateWorkTask;

public class UpdateWorkTaskCommandValidator : AbstractValidator<UpdateWorkTaskCommand>
{
    private readonly IWorkTaskRepository _workTaskRepository;
    private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityTypeRepository;
    private readonly IWorkTaskStatusTypeRepository _workTaskStatusTypeRepository;

    public UpdateWorkTaskCommandValidator(IWorkTaskRepository workTaskRepository, IWorkTaskPriorityTypeRepository workTaskPriorityTypeRepository, IWorkTaskStatusTypeRepository workTaskStatusTypeRepository)
    {
        _workTaskRepository = workTaskRepository;
        _workTaskPriorityTypeRepository = workTaskPriorityTypeRepository;
        _workTaskStatusTypeRepository = workTaskStatusTypeRepository;

        Include(new BaseWorkTaskCommandValidator(_workTaskPriorityTypeRepository, _workTaskStatusTypeRepository));

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(WorkTaskMustExist).WithMessage("{PropertyName} does not exist");
    }

    private async Task<bool> WorkTaskMustExist(int id, CancellationToken token)
    {
        var workTask = _workTaskRepository.GetByIdAsync(id);
        return workTask != null;
    }
}
