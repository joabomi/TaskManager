using AutoMapper;
using MediatR;
using TaskManager.Application.Contracts.Identity;
using TaskManager.Application.Contracts.Logging;
using TaskManager.Application.Features.WorkTaskUser.Queries.GetAllWorkTaskUsers;

namespace TaskManager.Application.Features.WorkTaskUser.Queries.GetWorkTaskUserDetails;

public class GetWorkTaskUserDetailsQueryHandler : IRequestHandler<GetWorkTaskUserDetailsQuery, WorkTaskUserDetailsDto>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetAllWorkTaskUserQueryHandler> _logger;

    public GetWorkTaskUserDetailsQueryHandler(IUserService userService, IMapper mapper, IAppLogger<GetAllWorkTaskUserQueryHandler> logger)
    {
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<WorkTaskUserDetailsDto> Handle(GetWorkTaskUserDetailsQuery request, CancellationToken cancellationToken)
    {
        //query the database
        var taskManagerUser = await _userService.GetUser(request.userId);
        //convert data to DTO
        var ret_val = _mapper.Map<WorkTaskUserDetailsDto>(taskManagerUser);
        //return list DTO objects
        _logger.LogInformation("WorkTask User details was retrieved successfully");
        return ret_val;
    }
}
