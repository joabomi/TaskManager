using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Application.Contracts.Persistence;

public interface IPaginationParameters
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}
