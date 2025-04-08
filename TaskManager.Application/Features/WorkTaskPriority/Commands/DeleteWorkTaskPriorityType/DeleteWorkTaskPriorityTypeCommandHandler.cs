using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistence;

namespace TaskManager.Application.Features.WorkTaskPriority.Commands.DeleteWorkTaskPriorityType;

internal class DeleteWorkTaskPriorityTypeCommandHandler : IRequestHandler<DeleteWorkTaskPriorityTypeCommand, Unit>
{
    private readonly Mapper _mapper;
    private readonly IWorkTaskPriorityTypeRepository _workTaskPriorityRepository;

    public DeleteWorkTaskPriorityTypeCommandHandler(Mapper mapper, IWorkTaskPriorityTypeRepository workTaskPriorityRepository)
    {
        _mapper = mapper;
        _workTaskPriorityRepository = workTaskPriorityRepository;
    }

    public async Task<Unit> Handle(DeleteWorkTaskPriorityTypeCommand request, CancellationToken cancellationToken)
    {
        //Convert to domain entity object
        var workPriorityTypeToDelete = _mapper.Map<Domain.WorkTaskPriorityType>(request);
        //add to database
        await _workTaskPriorityRepository.DeleteAsync(workPriorityTypeToDelete);
        //return record id
        return Unit.Value;
    }
}
