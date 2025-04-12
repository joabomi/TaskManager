using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TaskManager.Application.Contracts.Identity;
using TaskManager.Application.Models.Identity;
using TaskManager.Identity.Models;

namespace TaskManager.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<TaskManagerUser> GetUser(string userId)
    {
        var taskManagerUser = await _userManager.FindByIdAsync(userId);
        return new TaskManagerUser
        {
            Email = taskManagerUser.Email,
            Id = taskManagerUser.Id,
            Firstname = taskManagerUser.FirstName,
            Lastname = taskManagerUser.LastName
        };
    }

    public async Task<List<TaskManagerUser>> GetUsers()
    {
        var taskManagerUsers = await _userManager.GetUsersInRoleAsync("TaskManagerUser");
        return taskManagerUsers.Select( q => new TaskManagerUser
        {
            Id = q.Id,
            Email = q.Email,
            Firstname= q.FirstName,
            Lastname= q.LastName
        }).ToList();
    }
}
