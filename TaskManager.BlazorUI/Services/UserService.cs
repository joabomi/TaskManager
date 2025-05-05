using AutoMapper;
using Blazored.LocalStorage;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models;
using TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;
using TaskManager.BlazorUI.Services.Base;

namespace TaskManager.BlazorUI.Services;

public class UserService : BaseHttpService, IUserService
{
    private readonly IMapper _mapper;

    public UserService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client, localStorage)
    {
        _mapper = mapper;
    }

    public async Task<List<UserVM>> GetUsers()
    {
        var usersList = await _client.UsersAllAsync();
        return _mapper.Map<List<UserVM>>(usersList);
    }

    public async Task<UserVM> GetUser(string id)
    {
        var user = await _client.UsersAsync(id);
        return _mapper.Map<UserVM>(user);
    }
}
