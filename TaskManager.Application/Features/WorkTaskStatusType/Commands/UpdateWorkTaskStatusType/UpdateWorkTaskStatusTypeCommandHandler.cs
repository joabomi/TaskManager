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
using TaskManager.Application.Features.WorkTaskStatusType.Commands.UpdateWorkStatusType;

namespace TaskManager.Application.Features.WorkTaskStatusType.Commands.UpdateWorkTaskStatusType;

public class UpdateWorkTaskStatusTypeCommandHandler: IRequestHandler<UpdateWorkTaskStatusTypeCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskStatusTypeRepository _workTaskStatusTypeRepository;
    private readonly IAppLogger<UpdateWorkTaskStatusTypeCommandHandler> _logger;

    public UpdateWorkTaskStatusTypeCommandHandler(IMapper mapper,
        IWorkTaskStatusTypeRepository workTaskStatusRepository,
        IAppLogger<UpdateWorkTaskStatusTypeCommandHandler> logger)
    {
        _mapper = mapper;
        _workTaskStatusTypeRepository = workTaskStatusRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateWorkTaskStatusTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate data incoming
        var validator = new UpdateWorkTaskStatusTypeCommandValidator(_workTaskStatusTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}", nameof(WorkTaskStatusType), request.Id);
            throw new BadRequestException("Invalid WorkTask Status Type", validationResult);
        }

        //Convert to domain entity object
        var workStatusTypeToUpdate = _mapper.Map<Domain.WorkTaskStatusType>(request);
        //add to database
        await _workTaskStatusTypeRepository.UpdateAsync(workStatusTypeToUpdate);
        //return
        _logger.LogInformation("WorkTask Status Type successfully updated (ID: {0})",request.Id);
        return Unit.Value;
    }
}
