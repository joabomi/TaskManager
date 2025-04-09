using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TaskManager.Application.Contracts.Persistence;

namespace TaskManager.Application.Features.WorkTaskPriorityType.Commands.UpdateWorkTaskPriorityType;

public class UpdateWorkTaskPriorityTypeCommandValidator : AbstractValidator<UpdateWorkTaskPriorityTypeCommand>
{
    private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityTypeRepository;

    public UpdateWorkTaskPriorityTypeCommandValidator(IWorkTaskPriorityTypeRepository workTaskPriorityTypeRepository)
    {
        _workTaskPriorityTypeRepository = workTaskPriorityTypeRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(WorkTaskPriorityTypeMustExist);

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must be fewer than 100 characteres");

        RuleFor(p => p)
            .MustAsync(WorkStatusTypeNameUnique).WithMessage("Work Status Type already exists");

        RuleFor(p => p.PriorityWeight)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} cannot be less than 0")
            .LessThanOrEqualTo(1000).WithMessage("{PropertyName} cannot exceed 1000"); ;
    }

    private async Task<bool> WorkTaskPriorityTypeMustExist(int id, CancellationToken token)
    {
        var workTaskPriorityType = await _workTaskPriorityTypeRepository.GetByIdAsync(id);
        return workTaskPriorityType != null;
    }

    private async Task<bool> WorkStatusTypeNameUnique(UpdateWorkTaskPriorityTypeCommand command, CancellationToken token)
    {
        return await _workTaskPriorityTypeRepository.IsWorkPriorityTypeUnique(command.Name);
    }
}
