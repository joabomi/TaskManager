using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;

namespace TaskManager.Application.Features.WorkTask.Commands.UpdateWorkTask;

public class UpdateWorkTaskCommandHandler : IRequestHandler<UpdateWorkTaskCommand, Unit>
{
    private readonly IWorkTaskRepository _workTaskRepository;
    private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityTypeRepository;
    private readonly IWorkTaskStatusTypeRepository _workTaskStatusTypeRepository;
    private readonly IMapper _mapper;

    public UpdateWorkTaskCommandHandler(IWorkTaskRepository workTaskRepository, 
        IWorkTaskPriorityTypeRepository workTaskPriorityTypeRepository, IWorkTaskStatusTypeRepository workTaskStatusTypeRepository,
        IMapper mapper)
    {
        _workTaskRepository = workTaskRepository;
        _workTaskPriorityTypeRepository = workTaskPriorityTypeRepository;
        _workTaskStatusTypeRepository = workTaskStatusTypeRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateWorkTaskCommand request, CancellationToken cancellationToken)
    {
        var workTask = await _workTaskRepository.GetByIdAsync(request.Id);
        if (workTask == null)
            throw new NotFoundException(nameof(workTask), request.Id);

        var validator = new UpdateWorkTaskCommandValidator(_workTaskRepository, _workTaskPriorityTypeRepository, _workTaskStatusTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid WorkTask Request", validationResult);

        _mapper.Map(request, workTask);
        await _workTaskRepository.UpdateAsync(workTask);

        return Unit.Value;
    }
}
