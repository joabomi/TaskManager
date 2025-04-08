using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTaskStatus.Queries.GetAllWorkTaskStatusTypes;

namespace TaskManager.Application.Features.WorkTaskStatus.Queries.GetWorkTaskStatusTypeDetails;

public class GetWorkTaskStatusTypeDetailsHandler : IRequestHandler<GetWorkTaskStatusTypeDetailsQuery, WorkTaskStatusTypeDetailsDto>
{
    private readonly Mapper _mapper;
    private readonly IWorkTaskPriorityTypeRepository _workTaskStatusRepository;

    public GetWorkTaskStatusTypeDetailsHandler(Mapper mapper, IWorkTaskPriorityTypeRepository workTaskStatusRepository)
    {
        _mapper = mapper;
        _workTaskStatusRepository = workTaskStatusRepository;
    }

    public async Task<WorkTaskStatusTypeDetailsDto> Handle(GetWorkTaskStatusTypeDetailsQuery request, CancellationToken cancellationToken)
    {
        //query the database
        var workTaskStatusTypeDetails = await _workTaskStatusRepository.GetByIdAsync(request.Id);
        //convert data to DTO
        var ret_val = _mapper.Map<WorkTaskStatusTypeDetailsDto>(workTaskStatusTypeDetails);
        //return list DTO objects
        return ret_val;
    }
}
