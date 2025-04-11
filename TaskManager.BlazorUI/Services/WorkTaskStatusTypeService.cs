using TaskManager.BlazorUI.Contracts;
using TaskManager.BlazorUI.Services.Base;

namespace TaskManager.BlazorUI.Services
{
    public class WorkTaskStatusTypeService : BaseHttpService, IWorkTaskStatusTypeService
    {
        public WorkTaskStatusTypeService(IClient client) : base(client)
        {
        }
    }
}
