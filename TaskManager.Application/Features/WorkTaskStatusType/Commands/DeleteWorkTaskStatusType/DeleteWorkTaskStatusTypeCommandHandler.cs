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
    private readonly IWorkTaskStatusTypeRepository _workTaskStatusTypeRepository;
    private readonly IAppLogger<DeleteWorkTaskStatusTypeCommandHandler> _logger;

    public DeleteWorkTaskStatusTypeCommandHandler(IWorkTaskStatusTypeRepository workTaskStatusRepository,
        IAppLogger<DeleteWorkTaskStatusTypeCommandHandler> logger)
    {
        _workTaskStatusTypeRepository = workTaskStatusRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteWorkTaskStatusTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate data incoming
        if (request.Id < 0)
            throw new BadRequestException("Id not provided");

        var workTaskStatusTypeToDelete = await _workTaskStatusTypeRepository.GetByIdAsync(request.Id);
        if (workTaskStatusTypeToDelete == null)
            throw new NotFoundException(nameof(workTaskStatusTypeToDelete), request.Id);

        //add to database
        await _workTaskStatusTypeRepository.DeleteAsync(workTaskStatusTypeToDelete);

        //return
        _logger.LogInformation("WorkTask Status Type successfully deleted (ID: {0})", request.Id);
        return Unit.Value;
    }
}
