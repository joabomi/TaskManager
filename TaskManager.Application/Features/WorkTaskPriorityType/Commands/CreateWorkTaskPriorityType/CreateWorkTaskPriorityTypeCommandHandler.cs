using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Exceptions;
using TaskManager.Application.Features.WorkTaskStatusType.Commands.CreateWorkTaskStatusType;

namespace TaskManager.Application.Features.WorkTaskPriorityType.Commands.CreateWorkTaskPriorityType;

public class CreateWorkTaskPriorityTypeCommandHandler : IRequestHandler<CreateWorkTaskPriorityTypeCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityRepository;
    private readonly IAppLogger<CreateWorkTaskPriorityTypeCommandHandler> _logger;

    public CreateWorkTaskPriorityTypeCommandHandler(IMapper mapper,
        IWorkTaskPriorityTypeRepository workTaskPriorityRepository,
        IAppLogger<CreateWorkTaskPriorityTypeCommandHandler> logger)
    {
        _mapper = mapper;
        _workTaskPriorityRepository = workTaskPriorityRepository;
        _logger = logger;
    }

    public async Task<int> Handle(CreateWorkTaskPriorityTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate data incoming
        var validator = new CreateWorkTaskPriorityTypeCommandValidator(_workTaskPriorityRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0}", nameof(WorkTaskPriorityType));
            throw new BadRequestException("Invalid WorkTask Priority Type", validationResult);
        }
        //Convert to domain entity object
        var workTaskPriorityTypeToCreate = _mapper.Map<Domain.WorkTaskPriorityType>(request);
        //add to database
        await _workTaskPriorityRepository.CreateAsync(workTaskPriorityTypeToCreate);
        //return record id
        _logger.LogInformation("WorkTask Priority Type successfully created (ID: {0})", workTaskPriorityTypeToCreate.Id);
        return workTaskPriorityTypeToCreate.Id;
    }
}
