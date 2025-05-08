using MediatR;
using TaskManager.Application.Features.Common;

namespace TaskManager.Application.Features.WorkTaskPriorityType.Queries.GetAllWorkTaskPriorityTypes;

public record GetAllWorkTaskPriorityTypesQuery : BaseQuery, IRequest<List<WorkTaskPriorityTypeDto>>
 {
   public string? Name_Filter { get; set; }
   public int? MinWeight_Filter { get; set; }
   public int? MaxWeight_Filter { get; set; }
}
