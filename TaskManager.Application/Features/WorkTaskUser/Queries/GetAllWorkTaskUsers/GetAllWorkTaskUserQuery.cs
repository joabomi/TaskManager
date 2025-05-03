using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TaskManager.Application.Features.WorkTaskUser.Queries.GetAllWorkTaskUsers;

public record GetAllWorkTaskUserQuery() : IRequest<List<WorkTaskUserDto>>;
