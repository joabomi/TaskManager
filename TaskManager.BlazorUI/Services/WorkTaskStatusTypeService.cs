using AutoMapper;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models.WorkTaskStatusTypes;
using TaskManager.BlazorUI.Services.Base;

namespace TaskManager.BlazorUI.Services
{
    public class WorkTaskStatusTypeService : BaseHttpService, IWorkTaskStatusTypeService
    {
        private readonly IMapper _mapper;

        public WorkTaskStatusTypeService(IClient client, IMapper mapper) : base(client)
        {
            _mapper = mapper;
        }

        public async Task<Response<Guid>> CreateWorkTaskStatusType(WorkTaskStatusTypeVM workTaskStatusType)
        {
            try
            {
                var createWorkTaskStatusTypeCommand = _mapper.Map<CreateWorkTaskStatusTypeCommand>(workTaskStatusType);
                await _client.WorkTaskStatusTypesPOSTAsync(createWorkTaskStatusTypeCommand);
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

        public async Task<Response<Guid>> DeleteWorkTaskStatusType(int id)
        {
            try
            {
                await _client.WorkTaskStatusTypesDELETEAsync(id.ToString());
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

        public Task<WorkTaskStatusTypeVM> GetWorkTaskStatusTypeDetails(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<WorkTaskStatusTypeVM>> GetWorkTaskStatusTypes()
        {
            var workTaskStatusTypes = await _client.WorkTasksAllAsync();
            return _mapper.Map<List<WorkTaskStatusTypeVM>>(workTaskStatusTypes);
        }

        public async Task<Response<Guid>> UpdateWorkTaskStatusType(int id, WorkTaskStatusTypeVM workTaskStatusType)
        {
            try
            {
                var updateWorkTaskStatusTypeCommand = _mapper.Map<UpdateWorkTaskStatusTypeCommand>(workTaskStatusType);
                await _client.WorkTaskStatusTypesPUTAsync(id.ToString(), updateWorkTaskStatusTypeCommand);
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
