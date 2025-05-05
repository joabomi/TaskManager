using AutoMapper;
using Blazored.LocalStorage;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models.WorkTasks;
using TaskManager.BlazorUI.Services.Base;

namespace TaskManager.BlazorUI.Services
{
    public class WorkTaskService : BaseHttpService, IWorkTaskService
    {
        private readonly IMapper _mapper;

        public WorkTaskService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client, localStorage)
        {
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateWorkTask(WorkTaskVM workTask)
        {
            try
            {
                var createWorkTaskCommand = _mapper.Map<CreateWorkTaskCommand>(workTask);
                var response = await _client.WorkTasksPOSTAsync(createWorkTaskCommand);
                return new Response<int>()
                {
                    Data = response,
                    Success = true,
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<Response<Guid>> DeleteWorkTask(int id)
        {
            try
            {
                await _client.WorkTasksDELETEAsync(id);
                return new Response<Guid>()
                {
                    Success = true,
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }

        public async Task<WorkTaskVM> GetWorkTaskDetails(int id)
        {
            var workTaskDetails = await _client.WorkTasksGETAsync(id);
            return _mapper.Map<WorkTaskVM>(workTaskDetails);
        }

        public async Task<List<WorkTaskVM>> GetAdminWorkTasks()
        {
            var workTasks = await _client.WorkTasksAllAsync(isLoggedUser: false, isLoggedAdmin: true);
            return _mapper.Map<List<WorkTaskVM>>(workTasks);
        }

        public async Task<List<WorkTaskVM>> GetUserWorkTasks()
        {
            var workTasks = await _client.WorkTasksAllAsync(isLoggedUser: true, isLoggedAdmin: false);
            return _mapper.Map<List<WorkTaskVM>>(workTasks);
        }

        public async Task<Response<Guid>> UpdateWorkTask(int id, WorkTaskVM workTask)
        {
            try
            {
                var updateworkTaskcommand = _mapper.Map<UpdateWorkTaskCommand>(workTask);
                await _client.WorkTasksPUTAsync(id, updateworkTaskcommand);
                return new Response<Guid>()
                {
                    Success = true,
                };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }
    }
}
