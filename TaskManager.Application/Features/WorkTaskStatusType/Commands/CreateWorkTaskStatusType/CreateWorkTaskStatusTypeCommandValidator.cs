using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TaskManager.Application.Contracts.Persistence;

namespace TaskManager.Application.Features.WorkTaskStatusType.Commands.CreateWorkTaskStatusType;

public class CreateWorkTaskStatusTypeCommandValidator : AbstractValidator<CreateWorkTaskStatusTypeCommand>
{
    private IWorkTaskStatusTypeRepository _workTaskStatusRepository { get; set; }

    public CreateWorkTaskStatusTypeCommandValidator(IWorkTaskStatusTypeRepository workTaskStatusRepository)
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must be fewer than 100 characteres");

        RuleFor(p => p)
            .MustAsync(WorkStatusTypeNameUnique).WithMessage("Work Status Type already exists");

        _workTaskStatusRepository = workTaskStatusRepository;
    }

    private async Task<bool> WorkStatusTypeNameUnique(CreateWorkTaskStatusTypeCommand command, CancellationToken token)
    {
        return await _workTaskStatusRepository.IsWorkStatusTypeUnique(command.Name);
    }
}
