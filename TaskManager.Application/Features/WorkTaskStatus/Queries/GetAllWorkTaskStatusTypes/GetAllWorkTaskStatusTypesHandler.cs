using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistence;

namespace TaskManager.Application.Features.WorkTaskStatus.Queries.GetAllWorkTaskStatusTypes;

public class GetAllWorkTaskStatusTypesHandler : IRequestHandler<GetAllWorkTaskStatusTypesQuery, List<WorkTaskStatusTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskPriorityTypeRepository _workTaskStatusRepository;

    public GetAllWorkTaskStatusTypesHandler(IMapper mapper, IWorkTaskPriorityTypeRepository workTaskStatusRepository)
    {
        _mapper = mapper;
        _workTaskStatusRepository = workTaskStatusRepository;
    }

    public async Task<List<WorkTaskStatusTypeDto>> Handle(GetAllWorkTaskStatusTypesQuery request, CancellationToken cancellationToken)
    {
        //query the database
        var workTaskStatusTypes = await _workTaskStatusRepository.GetAsync();
        //convert data to DTO
        var ret_val = _mapper.Map<List<WorkTaskStatusTypeDto>>(workTaskStatusTypes);
        //return list DTO objects
        return ret_val;
    }
}
