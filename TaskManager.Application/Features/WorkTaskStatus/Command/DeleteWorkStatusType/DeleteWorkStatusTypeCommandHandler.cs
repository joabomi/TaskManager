using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTaskStatus.Command.UpdateWorkStatusType;

namespace TaskManager.Application.Features.WorkTaskStatus.Command.DeleteWorkStatusType;

internal class DeleteWorkStatusTypeCommandHandler : IRequestHandler<DeleteWorkStatusTypeCommand, Unit>
{
    private readonly Mapper _mapper;
    private readonly IWorkTaskStatusRepository _workTaskStatusRepository;

    public DeleteWorkStatusTypeCommandHandler(Mapper mapper, IWorkTaskStatusRepository workTaskStatusRepository)
    {
        _mapper = mapper;
        _workTaskStatusRepository = workTaskStatusRepository;
    }

    public async Task<Unit> Handle(DeleteWorkStatusTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate data incoming

        //Convert to domain entity object
        var workStatusTypeToDelete = _mapper.Map<Domain.WorkTaskStatusType>(request);
        //add to database
        await _workTaskStatusRepository.DeleteAsync(workStatusTypeToDelete);
        //return record id
        return Unit.Value;
    }
}
