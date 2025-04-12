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
using TaskManager.Application.Features.WorkTaskStatusType.Commands.DeleteWorkTaskStatusType;

namespace TaskManager.Application.Features.WorkTaskPriorityType.Commands.DeleteWorkTaskPriorityType;

public class DeleteWorkTaskPriorityTypeCommandHandler : IRequestHandler<DeleteWorkTaskPriorityTypeCommand, Unit>
{
    private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityRepository;
    private readonly IAppLogger<DeleteWorkTaskPriorityTypeCommandHandler> _logger;

    public DeleteWorkTaskPriorityTypeCommandHandler(IWorkTaskPriorityTypeRepository workTaskPriorityRepository,
        IAppLogger<DeleteWorkTaskPriorityTypeCommandHandler> logger)
    {
        _workTaskPriorityRepository = workTaskPriorityRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteWorkTaskPriorityTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate data incoming

        if (request.Id < 0)
            throw new BadRequestException("Id not provided");

        var workTaskPriorityTypeToDelete = await _workTaskPriorityRepository.GetByIdAsync(request.Id);
        if (workTaskPriorityTypeToDelete == null)
            throw new NotFoundException(nameof(workTaskPriorityTypeToDelete), request.Id);

        //delete from database
        await _workTaskPriorityRepository.DeleteAsync(workTaskPriorityTypeToDelete);
        //return record id
        _logger.LogInformation("WorkTask Priority Type successfully deleted (ID: {0})", request.Id);
        return Unit.Value;
    }
}
