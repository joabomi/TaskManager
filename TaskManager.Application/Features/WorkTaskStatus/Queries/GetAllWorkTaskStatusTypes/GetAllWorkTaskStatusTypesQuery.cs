﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TaskManager.Application.Features.WorkTaskStatus.Queries.GetAllWorkTaskStatusTypes
{
    public record GetAllWorkTaskStatusTypesQuery : IRequest<List<WorkTaskStatusTypeDto>>;
}
