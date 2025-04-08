using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Persistence;
using TaskManager.Application.Features.WorkTaskStatus.Queries.GetWorkTaskStatusTypeDetails;

namespace TaskManager.Application.Features.WorkTask.Queries.GetWorkTaskDetails;

public class GetWorkTaskDetailsHandler : IRequestHandler<GetWorkTaskDetailsQuery, WorkTaskDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly IWorkTaskRepository _workTaskRepository;

    public GetWorkTaskDetailsHandler(IMapper mapper, IWorkTaskRepository workTaskRepository)
    {
        _mapper = mapper;
        _workTaskRepository = workTaskRepository;
    }

    public async Task<WorkTaskDetailsDto> Handle(GetWorkTaskDetailsQuery request, CancellationToken cancellationToken)
    {
        //query the database
        var workTaskDetails = await _workTaskRepository.GetWorkTaskWithDetails(request.id);
        //convert data to DTO
        var ret_val = _mapper.Map<WorkTaskDetailsDto>(workTaskDetails);
        //return list DTO objects
        return ret_val;
    }
}
