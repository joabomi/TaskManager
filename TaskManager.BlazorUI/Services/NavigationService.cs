using Microsoft.AspNetCore.Components;
using TaskManager.BlazorUI.Contracts;

namespace TaskManager.BlazorUI.Services
{
    public class NavigationService : INavigationService
    {
        private readonly NavigationManager _navigationManager;

        public NavigationService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public void NavigateTo(string url)
        {
            _navigationManager.NavigateTo(url);
        }

        public void GoBack()
        {
            var uri = new Uri(_navigationManager.Uri);
            var segments = uri.AbsolutePath
                .Trim('/')
                .Split('/')
                .Where(s => !IsDynamicParameter(s))
                .ToList();

            if (segments.Count > 0)
            {
                var target = "/" + segments[0];
                _navigationManager.NavigateTo(target);
            }
            else
            {
                _navigationManager.NavigateTo("/");
            }
        }

        private bool IsDynamicParameter(string segment)
        {
            return Guid.TryParse(segment, out _) || int.TryParse(segment, out _);
        }
    }
}
