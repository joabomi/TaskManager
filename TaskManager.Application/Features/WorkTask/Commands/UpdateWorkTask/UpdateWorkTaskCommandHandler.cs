using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Features.WorkTaskStatusType.Commands.UpdateWorkTaskStatusType;
using TaskManager.Domain;

namespace TaskManager.Application.Features.WorkTask.Commands.UpdateWorkTask;

public class UpdateWorkTaskCommandHandler : IRequestHandler<UpdateWorkTaskCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskRepository _workTaskRepository;
    private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityTypeRepository;
    private readonly IWorkTaskStatusTypeRepository _workTaskStatusTypeRepository;
    private readonly IAppLogger<UpdateWorkTaskCommandHandler> _logger;

    public UpdateWorkTaskCommandHandler(IMapper mapper,
        IWorkTaskRepository workTaskRepository, 
        IWorkTaskPriorityTypeRepository workTaskPriorityTypeRepository,
        IWorkTaskStatusTypeRepository workTaskStatusTypeRepository,
        IAppLogger<UpdateWorkTaskCommandHandler> logger)
    {
        _mapper = mapper;
        _workTaskRepository = workTaskRepository;
        _workTaskPriorityTypeRepository = workTaskPriorityTypeRepository;
        _workTaskStatusTypeRepository = workTaskStatusTypeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateWorkTaskCommand request, CancellationToken cancellationToken)
    {
        //Validate data incoming
        if (request.Id < 0)
            throw new BadRequestException("Id not provided");

        var workTask = await _workTaskRepository.GetByIdAsync(request.Id);
        if (workTask == null)
            throw new NotFoundException(nameof(workTask), request.Id);

        var validator = new UpdateWorkTaskCommandValidator(_workTaskRepository, _workTaskPriorityTypeRepository, _workTaskStatusTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(WorkTask), request.Id);
            throw new BadRequestException("Invalid WorkTask Request", validationResult);
        }

        //Convert to domain entity object
        _mapper.Map(request, workTask);
        //Add to database
        await _workTaskRepository.UpdateAsync(workTask);
        //return
        _logger.LogInformation("WorkTask successfully updated (ID: {0})", request.Id);
        return Unit.Value;
    }
}
