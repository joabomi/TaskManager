using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TaskManager.Application.Features.WorkTaskPriority.Queries.GetWorkTaskPriorityDetails
{
    public record GetWorkTaskPriorityTypeDetailsQuery(int id) : IRequest<WorkTaskPriorityTypeDetailsDto>;
}
