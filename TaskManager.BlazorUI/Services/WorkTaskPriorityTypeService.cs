using Blazored.LocalStorage;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Services.Base;

namespace TaskManager.BlazorUI.Services
{
    public class WorkTaskPriorityTypeService : BaseHttpService, IWorkTaskPriorityTypeService
    {
        public WorkTaskPriorityTypeService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
		{
        }
    }
}
