using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetWorkTaskPriorityDetails
{
    public record GetWorkTaskPriorityTypeDetailsQuery(int Id = -1) : IRequest<WorkTaskPriorityTypeDetailsDto>;
}
