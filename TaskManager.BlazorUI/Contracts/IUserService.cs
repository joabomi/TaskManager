using TaskManager.BlazorUI.Models;

namespace TaskManager.BlazorUI.Contracts;

public interface IUserService
{
    Task<List<UserVM>> GetUsers();
}
