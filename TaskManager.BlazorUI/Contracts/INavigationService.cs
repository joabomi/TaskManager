namespace TaskManager.BlazorUI.Contracts
{
    public interface INavigationService
    {
        void NavigateTo(string url);
        void GoBack();
    }
}
