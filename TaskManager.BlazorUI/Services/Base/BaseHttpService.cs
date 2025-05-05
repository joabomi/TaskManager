using Blazored.LocalStorage;
using TaskManager.BlazorUI.Services.Base;

public class BaseHttpService
{
    protected readonly IClient _client;
    protected readonly ILocalStorageService _localStorage;

    public BaseHttpService(IClient client, ILocalStorageService localStorage)
    {
        _client = client;
        _localStorage = localStorage;
    }

    protected Response<T> ConvertApiExceptions<T>(ApiException ex)
    {
        if (ex.StatusCode == 400)
        {
            return new Response<T>() { Message = "Invalid data was submitted.", ValidationErrors = ex.Response, Success = false };
        }
        else if (ex.StatusCode == 404)
        {
            return new Response<T>() { Message = "The record was not found.", Success = false };
        }
        else
        {
            return new Response<T>() { Message = "Something went wrong, please try again later.", Success = false };

        }
    }

    //protected async Task AddBearerToken()
    //{
    //    if(await _localStorage.ContainKeyAsync("token"))
    //    {
    //        _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));
    //    }
    //}
}