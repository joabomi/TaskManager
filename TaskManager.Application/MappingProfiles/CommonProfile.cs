using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.Application.Features.Common;
using TaskManager.Application.Models.Persistance;

namespace TaskManager.Application.MappingProfiles;

public class CommonProfile : Profile
{
    public CommonProfile()
    {
        CreateMap<BaseQuery, BaseQueryParameters>();
    }
}
