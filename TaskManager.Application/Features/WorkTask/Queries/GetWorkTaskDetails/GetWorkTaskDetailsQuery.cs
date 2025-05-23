﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManager.Application.Features.WorkTaskStatusType.Queries.GetWorkTaskStatusTypeDetails;

namespace TaskManager.Application.Features.WorkTask.Queries.GetWorkTaskDetails;

public record GetWorkTaskDetailsQuery(int Id) : IRequest<WorkTaskDetailsDto>;
