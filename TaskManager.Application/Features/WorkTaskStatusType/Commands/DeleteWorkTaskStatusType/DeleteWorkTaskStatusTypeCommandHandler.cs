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
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetAllWorkTaskStatusTypes;

namespace TaskManager.Application.Features.WorkTaskStatusType.Commands.DeleteWorkTaskStatusType;

public class DeleteWorkTaskStatusTypeCommandHandler : IRequestHandler<DeleteWorkTaskStatusTypeCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskStatusTypeRepository _workTaskStatusTypeRepository;
    private readonly IAppLogger<DeleteWorkTaskStatusTypeCommandHandler> _logger;

    public DeleteWorkTaskStatusTypeCommandHandler(IMapper mapper,
        IWorkTaskStatusTypeRepository workTaskStatusRepository,
        IAppLogger<DeleteWorkTaskStatusTypeCommandHandler> logger)
    {
        _mapper = mapper;
        _workTaskStatusTypeRepository = workTaskStatusRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteWorkTaskStatusTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate data incoming
        if (request.Id < 0)
            throw new BadRequestException("Id not provided");

        var workTaskStatusType_exists = await _workTaskStatusTypeRepository.GetByIdAsync(request.Id);
        if (workTaskStatusType_exists == null)
            throw new NotFoundException(nameof(workTaskStatusType_exists), request.Id);

        //Convert to domain entity object
        var workStatusTypeToDelete = _mapper.Map<Domain.WorkTaskStatusType>(request);
        //add to database
        await _workTaskStatusTypeRepository.DeleteAsync(workStatusTypeToDelete);
        //return
        _logger.LogInformation("WorkTask Status Type successfully deleted (ID: {0})", request.Id);
        return Unit.Value;
    }
}
