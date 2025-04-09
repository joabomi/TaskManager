using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTaskStatusType.Commands.UpdateWorkTaskStatusType;

namespace TaskManager.Application.Features.WorkTaskStatusType.Commands.UpdateWorkStatusType;

public class UpdateWorkTaskStatusTypeCommandValidator : AbstractValidator<UpdateWorkTaskStatusTypeCommand>
{
    private readonly IWorkTaskStatusTypeRepository _workTaskStatusTypeRepository;

    public UpdateWorkTaskStatusTypeCommandValidator(IWorkTaskStatusTypeRepository workTaskStatusTypeRepository)
    {
        _workTaskStatusTypeRepository = workTaskStatusTypeRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(WorkTaskStatusTypeMustExist);

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must be fewer than 100 characteres");

        RuleFor(p => p)
            .MustAsync(WorkStatusTypeNameUnique).WithMessage("Work Status Type already exists");
    }

    private async Task<bool> WorkTaskStatusTypeMustExist(int id, CancellationToken token)
    {
        var workTaskStatusType = await _workTaskStatusTypeRepository.GetByIdAsync(id);
        return workTaskStatusType != null;
    }

    private async Task<bool> WorkStatusTypeNameUnique(UpdateWorkTaskStatusTypeCommand command, CancellationToken token)
    {
        return await _workTaskStatusTypeRepository.IsWorkStatusTypeUnique(command.Name);
    }
}
