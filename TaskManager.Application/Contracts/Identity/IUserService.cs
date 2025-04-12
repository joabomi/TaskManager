using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Models.Identity;

namespace TaskManager.Application.Contracts.Identity;

public interface IUserService
{
    Task<List<TaskManagerUser>> GetUsers();
    Task<TaskManagerUser> GetUser(string userId);
}
