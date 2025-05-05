
using AutoMapper;
using TaskManager.Application.Features.WorkTaskUser.Queries.GetAllWorkTaskUsers;
using TaskManager.Application.Features.WorkTaskUser.Queries.GetWorkTaskUserDetails;
using TaskManager.Application.Models.Identity;
using TaskManager.Domain;

namespace TaskManager.Application.MappingProfiles;

public class WorkTaskUsersProfile : Profile
{
    public WorkTaskUsersProfile()
    {
        CreateMap<WorkTaskUserDto, TaskManagerUser>().ReverseMap();
        CreateMap<WorkTaskUserDetailsDto, TaskManagerUser>().ReverseMap();
    }
}
