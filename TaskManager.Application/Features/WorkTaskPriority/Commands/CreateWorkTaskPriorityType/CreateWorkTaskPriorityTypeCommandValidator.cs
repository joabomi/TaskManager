using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TaskManager.Application.Contracts.Persistence;

namespace TaskManager.Application.Features.WorkTaskPriority.Commands.CreateWorkTaskPriorityType;

public class CreateWorkTaskPriorityTypeCommandValidator : AbstractValidator<CreateWorkTaskPriorityTypeCommand>
{
    private IWorkTaskPriorityTypeRepository _workTaskPriorityRepository { get; set; }

    public CreateWorkTaskPriorityTypeCommandValidator(IWorkTaskPriorityTypeRepository workTaskPriorityRepository)
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must be fewer than 70 characteres");

        RuleFor(p => p)
            .MustAsync(WorkPriorityTypeNameUnique).WithMessage("Work Priority Type already exists");

        RuleFor(p => p.PriorityWeight)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} cannot be less than 0")
            .LessThanOrEqualTo(1000).WithMessage("{PropertyName} cannot exceed 1000"); ;

        _workTaskPriorityRepository = workTaskPriorityRepository;
    }

    private Task<bool> WorkPriorityTypeNameUnique(CreateWorkTaskPriorityTypeCommand command, CancellationToken token)
    {
        return _workTaskPriorityRepository.IsWorkPriorityTypeUnique(command.Name);
    }
}
