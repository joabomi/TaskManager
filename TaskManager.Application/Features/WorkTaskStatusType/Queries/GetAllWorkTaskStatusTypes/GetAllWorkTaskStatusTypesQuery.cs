using MediatR;
using TaskManager.Application.Features.Common;

namespace TaskManager.Application.Features.WorkTaskStatusType.Queries.GetAllWorkTaskStatusTypes
{
    public record GetAllWorkTaskStatusTypesQuery : BaseQuery, IRequest<List<WorkTaskStatusTypeDto>>
    {
        public string? Name_Filter { get; set; }
    }
}
