using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using TaskManager.Application.Contracts.Persistence;

namespace TaskManager.Application.Features.WorkTask.Shared;

public class BaseWorkTaskCommandValidator : AbstractValidator<BaseWorkTaskCommand>
{
    private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityTypeRepository;
    private readonly IWorkTaskStatusTypeRepository _workTaskStatusTypeRepository;

    public BaseWorkTaskCommandValidator(IWorkTaskPriorityTypeRepository workTaskPriorityTypeRepository, IWorkTaskStatusTypeRepository workTaskStatusTypeRepository)
    {
        _workTaskPriorityTypeRepository = workTaskPriorityTypeRepository;
        _workTaskStatusTypeRepository = workTaskStatusTypeRepository;

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must be fewer than 100 characteres.");

        RuleFor(p => p.Description)
            .NotNull()
            .MaximumLength(500).WithMessage("{PropertyName} must be fewer than 500 characters");

        RuleFor(p => p.StartDate)
            .LessThanOrEqualTo(p => p.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}.");

        RuleFor(p => p.EndDate)
            .GreaterThanOrEqualTo(p => p.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}.");

        RuleFor(p => p.PriorityId)
            .GreaterThanOrEqualTo(0)
            .MustAsync(TaskPriorityMustExist).WithMessage("{PropertyName} does not exist.");

        RuleFor(p => p.StatusId)
            .GreaterThanOrEqualTo(0)
            .MustAsync(TaskStatusMustExist).WithMessage("{PropertyName} does not exist.");

        //to do: validation of AssignedPersonId field
    }

    private async Task<bool> TaskPriorityMustExist(int id, CancellationToken token)
    {
        var taskPriority = await _workTaskPriorityTypeRepository.GetByIdAsync(id);
        return taskPriority != null;
    }

    private async Task<bool> TaskStatusMustExist(int id, CancellationToken token)
    {
        var taskStatus = await _workTaskStatusTypeRepository.GetByIdAsync(id);
        return taskStatus != null;
    }
}
