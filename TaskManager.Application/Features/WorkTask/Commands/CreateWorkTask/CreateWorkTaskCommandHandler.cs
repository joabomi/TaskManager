using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;

namespace TaskManager.Application.Features.WorkTask.Commands.CreateWorkTask;

public class CreateWorkTaskCommandHandler : IRequestHandler<CreateWorkTaskCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskRepository _workTaskRepository;
    private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityTypeRepository;
    private readonly IWorkTaskStatusTypeRepository _workTaskStatusTypeRepository;
    private readonly IAppLogger<CreateWorkTaskCommandHandler> _logger;

    public CreateWorkTaskCommandHandler(IMapper mapper,
        IWorkTaskRepository workTaskRepository,
        IWorkTaskPriorityTypeRepository workTaskPriorityTypeRepository,
        IWorkTaskStatusTypeRepository workTaskStatusTypeRepository,
        IAppLogger<CreateWorkTaskCommandHandler> logger)
    {
        _mapper = mapper;
        _workTaskRepository = workTaskRepository;
        _workTaskPriorityTypeRepository = workTaskPriorityTypeRepository;
        _workTaskStatusTypeRepository = workTaskStatusTypeRepository;
        _logger = logger;
    }

    public async Task<int> Handle(CreateWorkTaskCommand request, CancellationToken cancellationToken)
    {
        //Validate data
        var validator = new CreateWorkTaskCommandValidator(_workTaskPriorityTypeRepository, _workTaskStatusTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0}", nameof(WorkTask));
            throw new BadRequestException("Invalid WorkTask request", validationResult);
        }

        //Convert to domain entity object
        var workTaskToCreate = _mapper.Map<Domain.WorkTask>(request);
        //add to database
        await _workTaskRepository.CreateAsync(workTaskToCreate);
        //return record id
        _logger.LogInformation("WorkTask successfully created (ID: {0})", workTaskToCreate.Id);
        return workTaskToCreate.Id;
    }
}
