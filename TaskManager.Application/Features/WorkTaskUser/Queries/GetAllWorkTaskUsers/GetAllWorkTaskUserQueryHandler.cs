using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Identity;
using TaskManager.Application.Contracts.Logging;

namespace TaskManager.Application.Features.WorkTaskUser.Queries.GetAllWorkTaskUsers;

public class GetAllWorkTaskUserQueryHandler : IRequestHandler<GetAllWorkTaskUserQuery, List<WorkTaskUserDto>>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetAllWorkTaskUserQueryHandler> _logger;

    public GetAllWorkTaskUserQueryHandler(IUserService userService, IMapper mapper, IAppLogger<GetAllWorkTaskUserQueryHandler> logger)
    {
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<WorkTaskUserDto>> Handle(GetAllWorkTaskUserQuery request, CancellationToken cancellationToken)
    {
        //query the database
        var taskManagerUser = await _userService.GetUsers();
        //convert data to DTO
        var ret_val = _mapper.Map<List<WorkTaskUserDto>>(taskManagerUser);
        //return list DTO objects
        _logger.LogInformation("WorkTask Users were retrieved successfully");
        return ret_val;
    }
}
