using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTask.Shared;
using TaskManager.Application.Features.WorkTaskPriorityType.Commands.CreateWorkTaskPriorityType;

namespace TaskManager.Application.Features.WorkTask.Commands.CreateWorkTask;

public class CreateWorkTaskCommandValidator :AbstractValidator<CreateWorkTaskCommand>
{
    private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityTypeRepository;
    private readonly IWorkTaskStatusTypeRepository _workTaskStatusTypeRepository;

    public CreateWorkTaskCommandValidator(IWorkTaskPriorityTypeRepository workTaskPriorityTypeRepository, IWorkTaskStatusTypeRepository taskStatusTypeRepository)
    {
        _workTaskPriorityTypeRepository = workTaskPriorityTypeRepository;
        _workTaskStatusTypeRepository = taskStatusTypeRepository;

        Include(new BaseWorkTaskCommandValidator(_workTaskPriorityTypeRepository, _workTaskStatusTypeRepository));
    }
}
