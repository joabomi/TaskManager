using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistence;

namespace TaskManager.Application.Features.WorkTaskStatus.Command.CreateWorkStatusType;

public class CreateWorkStatusTypeCommandHandler : IRequestHandler<CreateWorkStatusTypeCommand, int>
{
    private readonly Mapper _mapper;
    private readonly IWorkTaskStatusRepository _workTaskStatusRepository;

    public CreateWorkStatusTypeCommandHandler(Mapper mapper, IWorkTaskStatusRepository workTaskStatusRepository)
    {
        _mapper = mapper;
        _workTaskStatusRepository = workTaskStatusRepository;
    }

    public async Task<int> Handle(CreateWorkStatusTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate data incoming

        //Convert to domain entity object
        var workStatusTypeToCreate = _mapper.Map<Domain.WorkTaskStatusType>(request);
        //add to database
        await _workTaskStatusRepository.CreateAsync(workStatusTypeToCreate);
        //return record id
        return workStatusTypeToCreate.Id;
    }
}
