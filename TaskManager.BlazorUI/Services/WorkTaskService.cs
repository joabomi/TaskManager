using Blazored.LocalStorage;
using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Services.Base;

namespace TaskManager.BlazorUI.Services
{
    public class WorkTaskService : BaseHttpService, IWorkTaskService
    {
        public WorkTaskService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
        {
        }
    }
}
