﻿using AutoMapper;
using Blazored.LocalStorage;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Models.WorkTaskStatusTypes;
using TaskManager.BlazorUI.Services.Base;

namespace TaskManager.BlazorUI.Services
{
    public class WorkTaskStatusTypeService : BaseHttpService, IWorkTaskStatusTypeService
    {
        private readonly IMapper _mapper;

        public WorkTaskStatusTypeService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client, localStorage)
		{
            _mapper = mapper;
        }


        public async Task<Response<int>> CreateWorkTaskStatusType(WorkTaskStatusTypeVM workTaskStatusType)
        {
            try
            {
                var createWorkTaskStatusTypeCommand = _mapper.Map<CreateWorkTaskStatusTypeCommand>(workTaskStatusType);
                var response = await _client.WorkTaskStatusTypesPOSTAsync(createWorkTaskStatusTypeCommand);
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

        public async Task<Response<Guid>> DeleteWorkTaskStatusType(int id)
        {
            try
            {
                await _client.WorkTaskStatusTypesDELETEAsync(id);
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

        public async Task<WorkTaskStatusTypeVM> GetWorkTaskStatusTypeDetails(int id)
        {
            var workTaskStatusTypeDetails = await _client.WorkTaskStatusTypesGETAsync(id);
            return _mapper.Map<WorkTaskStatusTypeVM>(workTaskStatusTypeDetails);
        }

        public async Task<List<WorkTaskStatusTypeVM>> GetWorkTaskStatusTypes()
        {
            var workTaskStatusTypes = await _client.WorkTaskStatusTypesAllAsync();
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
                return ConvertApiExceptions<Guid>(ex);
            }
        }
    }
}
