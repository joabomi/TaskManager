using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TaskManager.Application.Features.WorkTaskStatusType.Queries.GetAllWorkTaskStatusTypes
{
    public record GetAllWorkTaskStatusTypesQuery : IRequest<List<WorkTaskStatusTypeDto>>;
}
