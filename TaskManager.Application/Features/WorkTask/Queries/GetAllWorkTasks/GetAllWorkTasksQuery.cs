using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetAllWorkTaskStatusTypes;

namespace TaskManager.Application.Features.WorkTask.Queries.GetAllWorkTasks;

public record GetAllWorkTasksQuery : IRequest<List<WorkTaskDto>>;
