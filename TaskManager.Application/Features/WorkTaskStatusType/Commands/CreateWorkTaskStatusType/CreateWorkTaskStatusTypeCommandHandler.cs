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

namespace TaskManager.Application.Features.WorkTaskStatusType.Commands.CreateWorkTaskStatusType;

public class CreateWorkTaskStatusTypeCommandHandler : IRequestHandler<CreateWorkTaskStatusTypeCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskStatusTypeRepository _workTaskStatusRepository;
    private readonly IAppLogger<CreateWorkTaskStatusTypeCommandHandler> _logger;

    public CreateWorkTaskStatusTypeCommandHandler(IMapper mapper,
        IWorkTaskStatusTypeRepository workTaskStatusRepository,
        IAppLogger<CreateWorkTaskStatusTypeCommandHandler> logger)
    {
        _mapper = mapper;
        _workTaskStatusRepository = workTaskStatusRepository;
        _logger = logger;
    }

    public async Task<int> Handle(CreateWorkTaskStatusTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate data incoming
        var validator = new CreateWorkTaskStatusTypeCommandValidator(_workTaskStatusRepository);
        var validationResult = await validator.ValidateAsync(request);
        if(validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in create request for {0}", nameof(WorkTaskStatusType));
            throw new BadRequestException("Invalid WorkTask Status Type", validationResult);
        }
        //Convert to domain entity object
        var workStatusTypeToCreate = _mapper.Map<Domain.WorkTaskStatusType>(request);
        //add to database
        await _workTaskStatusRepository.CreateAsync(workStatusTypeToCreate);
        //return record id
        _logger.LogInformation("WorkTask Status Type successfully created (ID: {0})", workStatusTypeToCreate.Id);
        return workStatusTypeToCreate.Id;
    }
}
