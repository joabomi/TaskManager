using AutoMapper;
using Blazored.LocalStorage;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models.WorkTaskPriorityTypes;
using TaskManager.BlazorUI.Services.Base;

namespace TaskManager.BlazorUI.Services
{
    public class WorkTaskPriorityTypeService : BaseHttpService, IWorkTaskPriorityTypeService
    {
        private readonly IMapper _mapper;

        public WorkTaskPriorityTypeService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client, localStorage)
        {
            _mapper = mapper;
        }


        public async Task<Response<int>> CreateWorkTaskPriorityType(WorkTaskPriorityTypeVM workTaskPriorityType)
        {
            try
            {
                await AddBearerToken();
                var createWorkTaskPriorityTypeCommand = _mapper.Map<CreateWorkTaskPriorityTypeCommand>(workTaskPriorityType);
                var response = await _client.WorkTaskPriorityTypesPOSTAsync(createWorkTaskPriorityTypeCommand);
                return new Response<int>()
                {
                    Data = response,
                    Success = true,
                };
            }
            catch (ApiException ex)
            {
                return ConverApiExceptions<int>(ex);
            }
        }

        public async Task<Response<Guid>> DeleteWorkTaskPriorityType(int id)
        {
            try
            {
                await AddBearerToken();
                await _client.WorkTaskPriorityTypesDELETEAsync(id);
                return new Response<Guid>()
                {
                    Success = true,
                };
            }
            catch (ApiException ex)
            {
                return ConverApiExceptions<Guid>(ex);
            }
        }

        public async Task<WorkTaskPriorityTypeVM> GetWorkTaskPriorityTypeDetails(int id)
        {
            await AddBearerToken();
            var workTaskPriorityTypeDetails = await _client.WorkTaskPriorityTypesGETAsync(id);
            return _mapper.Map<WorkTaskPriorityTypeVM>(workTaskPriorityTypeDetails);
        }

        public async Task<List<WorkTaskPriorityTypeVM>> GetWorkTaskPriorityTypes()
        {
            await AddBearerToken();
            var workTaskPriorityTypes = await _client.WorkTaskPriorityTypesAllAsync();
            return _mapper.Map<List<WorkTaskPriorityTypeVM>>(workTaskPriorityTypes);
        }

        public async Task<Response<Guid>> UpdateWorkTaskPriorityType(int id, WorkTaskPriorityTypeVM workTaskPriorityType)
        {
            try
            {
                await AddBearerToken();
                var updateWorkTaskPriorityTypeCommand = _mapper.Map<UpdateWorkTaskPriorityTypeCommand>(workTaskPriorityType);
                await _client.WorkTaskPriorityTypesPUTAsync(id.ToString(), updateWorkTaskPriorityTypeCommand);
                return new Response<Guid>()
                {
                    Success = true,
                };
            }
            catch (ApiException ex)
            {
                return ConverApiExceptions<Guid>(ex);
            }
        }
    }
}
