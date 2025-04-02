using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManager.Domain;

namespace TaskManager.Application.Features.WorkTaskStatus.Queries.GetWorkTaskStatusTypeDetails
{
    public record GetWorkTaskStatusTypeDetailsQuery(int Id) : IRequest<WorkTaskStatusTypeDetailsDto>;
}
