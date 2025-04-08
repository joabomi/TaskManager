using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistence;

namespace TaskManager.Application.Features.WorkTaskStatus.Commands.DeleteWorkTaskStatusType;

internal class DeleteWorkTaskStatusTypeCommandHandler : IRequestHandler<DeleteWorkTaskStatusTypeCommand, Unit>
{
    private readonly Mapper _mapper;
    private readonly IWorkTaskStatusTypeRepository _workTaskStatusRepository;

    public DeleteWorkTaskStatusTypeCommandHandler(Mapper mapper, IWorkTaskStatusTypeRepository workTaskStatusRepository)
    {
        _mapper = mapper;
        _workTaskStatusRepository = workTaskStatusRepository;
    }

    public async Task<Unit> Handle(DeleteWorkTaskStatusTypeCommand request, CancellationToken cancellationToken)
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
